using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Persistence.Inline;

public interface IWorkflowInstanceSerializerFactory
{
    IWorkflowInstanceSerializer Create(IWorkflowDefinition workflowDefinition);
}