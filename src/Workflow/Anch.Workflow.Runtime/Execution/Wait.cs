namespace Anch.Workflow.Execution;

public record Wait : IExecutionResult
{
    public bool LeaveState { get; } = false;
}