using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Engine;
using Anch.Workflow.Execution;

namespace Anch.Workflow.States;

public class PushEventState : IState
{
    public EventHeader Event { get; set; } = null!;

    public StateInstance? TargetState { get; set; }

    public object? Data { get; set; }

    public async ValueTask<IExecutionResult> Run(IExecutionContext executionContext)
    {
        if (executionContext.IsCallbackEvent)
        {
            return new Done();
        }

        return new PushEventResult(this.Event, this.TargetState, this.Data);
    }
}