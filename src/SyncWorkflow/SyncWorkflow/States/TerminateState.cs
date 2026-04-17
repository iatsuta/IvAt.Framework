using SyncWorkflow.Engine;
using SyncWorkflow.ExecutionResult;

namespace SyncWorkflow.States;

public class TerminateState : IState
{
    public async Task<IExecutionResult> Run(IExecutionContext executionContext)
    {
        return new MultiExecutionResult([
            new PushEventResult(EventHeader.WorkflowTerminated, null),
            new PushEventResult(EventHeader.WorkflowFinished, null)
        ]);
    }
}