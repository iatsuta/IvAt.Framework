using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Execution;

public record Done(WorkflowProcessResult WorkflowProcessResult) : IExecutionResult
{
    public Done()
        : this(WorkflowProcessResult.Empty)
    {
    }

    public bool LeaveState { get; } = true;
}