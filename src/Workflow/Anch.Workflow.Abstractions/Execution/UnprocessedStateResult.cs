using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Execution;

public record UnprocessedStateResult(StateInstance StateInstance, UnprocessedStateData Data);

public abstract record UnprocessedStateData;

public record InputUnprocessedStateData : UnprocessedStateData;

public record RunUnprocessedStateData : UnprocessedStateData;

public record LeaveUnprocessedStateData : UnprocessedStateData;

public record ExecutionResultUnprocessedStateData(IExecutionResult ExecutionResult) : UnprocessedStateData;