namespace Anch.Workflow.Domain.Runtime;

public record UnprocessWorkflowData(Func<Task<WorkflowProcessResult>> GetResult);