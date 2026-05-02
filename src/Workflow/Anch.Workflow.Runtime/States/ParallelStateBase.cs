using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Engine;
using Anch.Workflow.ExecutionResult;
using Anch.Workflow.States._Base;

namespace Anch.Workflow.States;

public abstract class ParallelStateBase<TSource> : IState
{
    public StateBreakPolicy BreakPolicy { get; set; } = StateBreakPolicy.WaitAll;

    public StateLeavePolicy LeavePolicy { get; set; } = StateLeavePolicy.TerminateChild;

    public async Task<IExecutionResult> Run(IExecutionContext executionContext)
    {
        if (!executionContext.IsCallbackEvent)
        {
            await this.Start(executionContext);
        }

        var notProcessedWorkflow = executionContext.StateInstance.Children.Where(i => i.Status.Role != WorkflowStatusRole.Finished).ToList();

        var isBreak = await this.BreakPolicy.CanBreak(executionContext);

        if (!isBreak && notProcessedWorkflow.Any())
        {
            return executionContext.IsCallbackEvent
                ? new Wait()
                : new MultiExecutionResult(notProcessedWorkflow.Select(subWf => new WaitEventResult(EventHeader.WorkflowFinished, subWf)));
        }
        else
        {
            return new Done();
        }
    }

    protected abstract IEnumerable<IWorkflowMachine> CreateChildMachines(TSource source);

    private async Task<WorkflowProcessResult> Start(IExecutionContext executionContext)
    {
        var currentState = executionContext.StateInstance;

        var childMachines = this.CreateChildMachines((TSource)executionContext.Source).ToList();

        foreach (var childrenMachine in childMachines)
        {
            var subWf = childrenMachine.WorkflowInstance;

            subWf.Owner = currentState;

            currentState.Children.Add(subWf);

            await childrenMachine.Save(executionContext.CancellationToken);
        }

        var result = new List<WorkflowProcessResult>();

        foreach (var childrenMachine in childMachines)
        {
            result.Add(await childrenMachine.ProcessWorkflow(executionContext.CancellationToken));
        }

        return result.Aggregate();
    }
}