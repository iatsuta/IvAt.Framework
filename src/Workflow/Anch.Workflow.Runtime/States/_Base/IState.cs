using Anch.Workflow.Engine;
using Anch.Workflow.Execution;

namespace Anch.Workflow.States._Base;

public interface IState
{
    StateLeavePolicy LeavePolicy => StateLeavePolicy.Forget;

    Task<IExecutionResult> Run(IExecutionContext executionContext);
}