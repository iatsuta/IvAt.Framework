using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Domain;

public record WaitEventInfo(
    EventHeader Header,
    WorkflowInstance? SourceWorkflow,
    StateInstance TargetState,
    object? Data = null)
{
    public bool Release()
    {
        return this.TargetState.WaitEvents.Remove(this);
    }
}