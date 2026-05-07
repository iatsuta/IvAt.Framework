using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Inline;

public interface IStateDefinitionResolver<in TSource>
{
    IStateDefinition GetCurrentStateDefinition(TSource source);
}