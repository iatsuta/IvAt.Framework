using Anch.Workflow.Domain;
using Anch.Workflow.Engine;
using Anch.Workflow.Execution;

namespace Anch.Workflow.States;

public class TaskState : IState
{
    public IReadOnlyList<EventHeader> CommandHeaders { get; set; } = null!;

    public async ValueTask<IExecutionResult> Run(IExecutionContext executionContext)
    {
        if (executionContext.IsCallbackEvent)
        {
            executionContext.StateInstance.ReleaseWaitEvents();

            return new PushEventResult(executionContext.CallbackEventInfo!.Header, null, executionContext.CallbackEventInfo!.Data);
        }
        else
        {
            return new MultiExecutionResult([.. this.CommandHeaders.Select(header => new WaitEventResult(header, null))]);
        }
    }

    public static readonly string CommandsKey = "Commands";
}