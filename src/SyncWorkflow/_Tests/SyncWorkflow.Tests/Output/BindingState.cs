using SyncWorkflow.Engine;
using SyncWorkflow.ExecutionResult;
using SyncWorkflow.States;

namespace SyncWorkflow.Tests.Binding;

public class BindingState : IState
{
    public int Value1 { get; set; }

    public int Value2 { get; set; }

    public int Result { get; set; }


    public async Task<IExecutionResult> Run(IExecutionContext executionContext)
    {
        this.Result = this.Value1 + this.Value2;

        return new Done();
    }
}