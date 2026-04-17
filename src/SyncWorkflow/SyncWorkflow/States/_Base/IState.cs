using SyncWorkflow.Engine;
using SyncWorkflow.ExecutionResult;

namespace SyncWorkflow.States;

public interface IState
{
    StateLeavePolicy LeavePolicy => StateLeavePolicy.Forget;

    Task<IExecutionResult> Run(IExecutionContext executionContext);
}