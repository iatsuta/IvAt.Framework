using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Persistence.Inline;

public interface IStateDefinitionResolver<in TSource>
{
    IStateDefinition GetCurrentStateDefinition(TSource source);
}