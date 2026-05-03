namespace Anch.Workflow.Execution;

public record WorkflowProcessExecutionResult(WorkflowProcessResult WorkflowProcessResult, bool LeaveState) : IExecutionResult
{
    //public bool LeaveState => this.ForceLeaveState ?? !this.WorkflowProcessResult.Unprocessed.Any();
}