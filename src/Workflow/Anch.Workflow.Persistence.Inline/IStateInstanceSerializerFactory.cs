using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Persistence.Inline;

public interface IStateInstanceSerializerFactory
{
    IStateInstanceSerializer Create(IWorkflowDefinition workflow);
}