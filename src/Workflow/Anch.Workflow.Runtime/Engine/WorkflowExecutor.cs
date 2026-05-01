using Anch.Workflow.Storage;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Engine;

public class WorkflowExecutor(
    IWorkflowHost workflowHost,
    IServiceProvider serviceProvider,
    IWorkflowStorage rootWorkflowStorage,
    WorkflowExecutionPolicy executionPolicy)
    : IWorkflowExecutor
{
    public async Task<WorkflowProcessResult> StartWorkflow<TSource, TWorkflow>(TSource source, CancellationToken cancellationToken = default)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>
    {
        return await this.StartWorkflow(source, serviceProvider.GetRequiredService<TWorkflow>(), cancellationToken);
    }

    public async Task<WorkflowProcessResult> StartWorkflow<TSource, TWorkflow>(TSource source, TWorkflow workflow,
        CancellationToken cancellationToken = default)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>
    {
        var machine = workflowHost.CreateMachine(source, workflow);

        await machine.Save(cancellationToken);

        return await machine
            .ProcessWorkflow(cancellationToken)
            .ApplyPolicy(executionPolicy);
    }

    public async Task<WorkflowProcessResult> PushEvent(PushEventInfo pushEventInfo, CancellationToken cancellationToken = default)
    {
        var waitEvents = await rootWorkflowStorage.GetWaitEvents(pushEventInfo, cancellationToken);

        foreach (var waitEventInfo in waitEvents)
        {
            waitEventInfo.Release();
        }

        return await waitEvents
            .Select(waitEventInfo => workflowHost
                .CreateMachine(waitEventInfo.TargetState.Workflow)
                .PushReleasedEvent(waitEventInfo with { Data = pushEventInfo.Data }, cancellationToken))
            .Aggregate()
            .ApplyPolicy(executionPolicy);
    }
}