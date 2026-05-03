using Anch.Workflow.Engine;
using Anch.Workflow.Execution;
using Anch.Workflow.States._Base;

namespace Anch.Workflow.States;

public class EmptyState : IState
{
    public async Task<IExecutionResult> Run(IExecutionContext executionContext)
    {
        return new Done();
    }
}