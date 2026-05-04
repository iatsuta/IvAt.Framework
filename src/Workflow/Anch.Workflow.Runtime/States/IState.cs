using Anch.Workflow.Engine;
using Anch.Workflow.Execution;

namespace Anch.Workflow.States;

public interface IState
{
    StateLeavePolicy LeavePolicy => StateLeavePolicy.Forget;

    ValueTask<IExecutionResult> Run(IExecutionContext executionContext);
}