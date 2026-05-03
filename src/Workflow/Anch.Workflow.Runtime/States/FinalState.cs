using Anch.Workflow.Domain;
using Anch.Workflow.Engine;
using Anch.Workflow.Execution;
using Anch.Workflow.States._Base;

namespace Anch.Workflow.States;

public class FinalState : IState
{
    public object? Result { get; set; }

    public async Task<IExecutionResult> Run(IExecutionContext executionContext)
    {
        return new PushEventResult(EventHeader.WorkflowFinished, null, this.Result);
    }
}