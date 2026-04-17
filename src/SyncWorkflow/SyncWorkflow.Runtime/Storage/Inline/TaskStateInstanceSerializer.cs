using SyncWorkflow.Domain.Runtime;
using SyncWorkflow.States;

namespace SyncWorkflow.Storage.Inline;

public class TaskStateInstanceSerializer : IStateInstanceSerializer<TaskState>
{
    public StateInstance Deserialize(TaskState source)
    {
        throw new NotImplementedException();
    }

    public void Serialize(StateInstance workflowInstance)
    {
        throw new NotImplementedException();
    }
}