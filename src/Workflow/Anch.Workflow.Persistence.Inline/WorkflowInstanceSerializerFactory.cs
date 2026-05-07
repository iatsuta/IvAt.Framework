using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Persistence.Inline;

public class WorkflowInstanceSerializerFactory(IServiceProxyFactory serviceProxyFactory) : IWorkflowInstanceSerializerFactory
{
    public IWorkflowInstanceSerializer Create(IWorkflowDefinition workflowDefinition) =>

        serviceProxyFactory.Create<IWorkflowInstanceSerializer>(
            typeof(WorkflowInstanceSerializer<>).MakeGenericType(workflowDefinition.SourceType),
            workflowDefinition);
}