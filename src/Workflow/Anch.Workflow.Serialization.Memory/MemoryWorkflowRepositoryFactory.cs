using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Memory;

public class MemoryWorkflowRepositoryFactory(IServiceProxyFactory serviceProxyFactory) : IWorkflowRepositoryFactory
{
    public IWorkflowRepository Create(WorkflowDefinitionIdentity workflowDefinitionIdentity) =>
        serviceProxyFactory.Create<MemoryWorkflowRepository>(workflowDefinitionIdentity);
}