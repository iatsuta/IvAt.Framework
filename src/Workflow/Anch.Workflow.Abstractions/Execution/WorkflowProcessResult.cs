using System.Collections.Immutable;

using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Execution;

public record WorkflowProcessResult(
    ImmutableList<WorkflowInstance> Modified,
    ImmutableList<UnprocessedStateResult> Unprocessed)
{
    public static WorkflowProcessResult operator +(WorkflowProcessResult result1, WorkflowProcessResult result2)
    {
        if (result1 == Empty)
        {
            return result2;
        }
        else if (result2 == Empty)
        {
            return result1;
        }
        else
        {
            return new[] { result1, result2 }.Aggregate();
        }
    }

    public static WorkflowProcessResult Empty { get; } = new([], []);
}