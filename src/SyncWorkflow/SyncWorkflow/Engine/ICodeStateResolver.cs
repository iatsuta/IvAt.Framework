using SyncWorkflow.Domain.Runtime;
using SyncWorkflow.States;

namespace SyncWorkflow.Engine;

public interface ICodeStateResolver
{
    IState Resolve(StateInstance stateInstance);
}