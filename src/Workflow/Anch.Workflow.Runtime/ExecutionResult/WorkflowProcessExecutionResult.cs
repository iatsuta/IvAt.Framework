using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.ExecutionResult;

public record WorkflowProcessExecutionResult(WorkflowProcessResult WorkflowProcessResult, bool LeaveState) : IExecutionResult
{
    //public bool LeaveState => this.ForceLeaveState ?? !this.WorkflowProcessResult.Unprocessed.Any();
}