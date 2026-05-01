using Anch.Core;

namespace Anch.Workflow.Engine;

public class WorkflowHost(IServiceProxyFactory serviceProxyFactory)
    : IWorkflowHost
{
    public IWorkflowExecutor CreateExecutor(WorkflowExecutionPolicy executionPolicy)
    {
        return serviceProxyFactory.Create<IWorkflowExecutor, WorkflowExecutor>(executionPolicy);
    }
}