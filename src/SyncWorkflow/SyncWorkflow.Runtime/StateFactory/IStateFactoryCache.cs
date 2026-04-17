using SyncWorkflow.Definition;

namespace SyncWorkflow.StateFactory;

public interface IStateFactoryCache
{
    IStateFactory GetStateFactory(IStateDefinition stateDefinition);
}