using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Domain.Runtime;

public class StateInstance
{
    public Guid Id { get; set; }

    public StateInstanceFullIdentity Identity => new(this.Id, this.Workflow.Definition.Identity);

    public bool IsActual => this.IsCurrent
                            && this.Workflow.Status.Role != WorkflowStatusRole.Finished
                            && !this.OutputProcessed;

    public bool IsCurrent => this.Workflow.CurrentState == this;

    public bool InputProcessed { get; set; }

    public bool OutputProcessed { get; set; }

    public required WorkflowInstance Workflow { get; set; }

    public required IStateDefinition Definition { get; set; }

    public List<WorkflowInstance> Children { get; set; } = [];

    public HashSet<WaitEventInfo> WaitEvents { get; set; } = [];


    public void RegisterWaitEvent(WaitEventInfo eventInfo)
    {
        if (this != eventInfo.TargetState)
        {
            throw new InvalidOperationException("Invalid target state");
        }

        if (!this.IsCurrent)
        {
            throw new InvalidOperationException("Not current state");
        }

        if (!this.WaitEvents.Add(eventInfo))
        {
            throw new InvalidOperationException("Duplicate event");
        }
    }

    public List<WaitEventInfo> ReleaseWaitEvents()
    {
        var result = this.WaitEvents.ToList();

        this.WaitEvents.Clear();

        return result;
    }
}