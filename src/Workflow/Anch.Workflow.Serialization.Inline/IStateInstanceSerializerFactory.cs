using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Inline;

public interface IStateInstanceSerializerFactory
{
    IStateInstanceSerializer Create(IWorkflowDefinition workflow);
}