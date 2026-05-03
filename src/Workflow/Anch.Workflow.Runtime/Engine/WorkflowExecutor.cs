using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Execution;
using Anch.Workflow.Serialization;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Engine;

public class WorkflowExecutor(
    IWorkflowMachineFactory workflowMachineFactory,
    IServiceProvider serviceProvider,
    IWorkflowStorage rootWorkflowStorage,
    WorkflowExecutionPolicy executionPolicy)
    : IWorkflowExecutor
{
    public async Task<WorkflowProcessResult> Start<TSource, TWorkflow>(TSource source, CancellationToken cancellationToken)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>
    {
        return await this.Start(source, serviceProvider.GetRequiredService<TWorkflow>(), cancellationToken);
    }

    public async Task<WorkflowProcessResult> Start<TSource>(TSource source, IWorkflow<TSource> workflow, CancellationToken cancellationToken)
        where TSource : notnull
    {
        var machine = workflowMachineFactory.Create(source, workflow);

        var preResult = await machine.ProcessWorkflow(cancellationToken);

        return await this.ProcessUnprocessed(preResult, true, cancellationToken);
    }

    public async Task<WorkflowProcessResult> PushEvent(PushEventInfo pushEventInfo, CancellationToken cancellationToken)
    {
        var waitEvents = await rootWorkflowStorage.GetWaitEvents(pushEventInfo, cancellationToken);

        foreach (var waitEventInfo in waitEvents)
        {
            waitEventInfo.Release();
        }

        var preResult = await waitEvents
            .Select(waitEventInfo => workflowMachineFactory
                .Create(waitEventInfo.TargetState.Workflow)
                .PushReleasedEvent(waitEventInfo with { Data = pushEventInfo.Data }, cancellationToken))
            .Aggregate();

        return await this.ProcessUnprocessed(preResult, true, cancellationToken);
    }

    public Task<WorkflowProcessResult> ProcessUnprocessed(WorkflowProcessResult workflowProcessResult, CancellationToken cancellationToken) =>
        this.ProcessUnprocessed(workflowProcessResult, false, cancellationToken);

    private async Task<WorkflowProcessResult> ProcessUnprocessed(WorkflowProcessResult workflowProcessResult, bool firstStepProcessed,
        CancellationToken cancellationToken)
    {
        var allowSinge = firstStepProcessed && executionPolicy == WorkflowExecutionPolicy.SingleStep;

        if (workflowProcessResult.Unprocessed.Count == 0 || allowSinge)
        {
            return workflowProcessResult;
        }
        else
        {
            var state = workflowProcessResult;

            do
            {
                var current = state.Unprocessed.First();

                var tail = state with { Unprocessed = [.. state.Unprocessed.Skip(1)] };

                var machine = workflowMachineFactory.Create(current.StateInstance.Workflow);

                var stepResult = await machine.ProcessWorkflow(current.ExecutionResult, cancellationToken);

                state = tail + stepResult;

            } while (state.Unprocessed.Any());

            return state;
        }
    }
}