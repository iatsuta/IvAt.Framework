using Anch.Workflow.Engine;
using Anch.Workflow.Execution;

namespace Anch.Workflow.States.Output;

public class WriteLineState(IDefaultOutput output) : IState
{
    public string Message { get; set; } = null!;

    public async ValueTask<IExecutionResult> Run(IExecutionContext executionContext)
    {
        await output.TextWriter.WriteLineAsync(this.Message);

        return new Done();
    }
}