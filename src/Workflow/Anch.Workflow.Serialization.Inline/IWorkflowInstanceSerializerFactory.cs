using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Inline;

public interface IWorkflowInstanceSerializerFactory
{
    IWorkflowInstanceSerializer Create(IWorkflowDefinition workflowDefinition);
}