using SyncWorkflow.Engine;
using SyncWorkflow.ExecutionResult;

namespace SyncWorkflow.States;

public class EmptyState : IState
{
    public async Task<IExecutionResult> Run(IExecutionContext executionContext)
    {
        return new Done();
    }
}