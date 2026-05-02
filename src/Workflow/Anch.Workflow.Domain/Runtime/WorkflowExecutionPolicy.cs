namespace Anch.Workflow.Domain.Runtime;

public record WorkflowExecutionPolicy(bool DeepExecute)
{
    public static WorkflowExecutionPolicy Full { get; } = new(true);

    public static WorkflowExecutionPolicy Head { get; } = new(false);
}