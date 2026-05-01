namespace Anch.Workflow.Engine;

public interface IWorkflowHost
{
    IWorkflowExecutor CreateExecutor(WorkflowExecutionPolicy executionPolicy);
}