using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Domain;

public record PushEventInfo(
    EventHeader Header,
    WorkflowInstance? SourceWorkflow,
    StateInstance? TargetState,
    object? Data = null)
{
    public bool IsMatched(WaitEventInfo waitEventInfo)
    {
        return waitEventInfo.Header == this.Header
               && (waitEventInfo.TargetState == this.TargetState || this.TargetState == null)
               && (waitEventInfo.SourceWorkflow == null || this.SourceWorkflow == waitEventInfo.SourceWorkflow)
               && (waitEventInfo.Data == null || Equals(waitEventInfo.Data, this.Data));
    }
}