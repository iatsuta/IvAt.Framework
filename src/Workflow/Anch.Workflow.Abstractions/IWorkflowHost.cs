using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Engine;

public interface IWorkflowHost
{
    IWorkflowExecutor CreateExecutor(WorkflowExecutionPolicy executionPolicy);
}