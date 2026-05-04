using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Execution;

public record UnprocessedStateResultBase;

public record UnprocessedStateResult(StateInstance StateInstance, IExecutionResult ExecutionResult) : UnprocessedStateResultBase;

public record UnprocessedCurrentStateResult(WorkflowInstance WorkflowInstance) : UnprocessedStateResultBase;
