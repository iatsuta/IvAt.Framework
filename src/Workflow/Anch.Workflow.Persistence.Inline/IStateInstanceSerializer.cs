using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Persistence.Inline;

//public interface IStateInstanceSerializer<in TSource, TState>;

public interface IStateInstanceSerializer
{
    StateInstance Deserialize(object source);

    void Serialize(StateInstance workflowInstance);
}