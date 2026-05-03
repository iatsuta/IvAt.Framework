using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Execution;

public record UnprocessedStateResult(StateInstance StateInstance, IExecutionResult ExecutionResult);