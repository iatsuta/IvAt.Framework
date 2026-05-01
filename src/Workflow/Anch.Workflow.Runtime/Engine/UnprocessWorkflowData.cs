namespace Anch.Workflow.Engine;

public record UnprocessWorkflowData(Func<Task<WorkflowProcessResult>> GetResult);