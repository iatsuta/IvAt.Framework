using SyncWorkflow.Definition;

namespace SyncWorkflow.Storage.Inline;

public interface IStateDefinitionResolver<in TSource>
{
    IStateDefinition GetCurrentStateDefinition(TSource source);
}