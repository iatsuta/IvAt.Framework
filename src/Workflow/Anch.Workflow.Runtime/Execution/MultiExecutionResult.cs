using System.Collections.Immutable;

namespace Anch.Workflow.Execution;

public record MultiExecutionResult(ImmutableArray<IExecutionResult> ExecutionResults) : IExecutionResult
{
    public bool LeaveState => this.ExecutionResults.All(er => er.LeaveState);
}