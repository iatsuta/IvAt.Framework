namespace Anch.Workflow.Execution;

public record WorkflowProcessExecutionResult(WorkflowProcessResult WorkflowProcessResult, bool IsDone) : IExecutionResult
{
    //public bool LeaveState => this.ForceLeaveState ?? !this.WorkflowProcessResult.Unprocessed.Any();
    public bool LeaveState => this.IsDone;
}