using Anch.Core;
using Anch.Workflow.Builder;
using Anch.Workflow.Builder.Default;

namespace Anch.Workflow.Tests.WriteLine;

public class WriteLineWorkflow : BuildWorkflow<object>
{
    public const string Message = "Hello world!";

    protected override void Build(IWorkflowBuilder<object, Ignore> builder)
    {
        builder.WriteLine(Message);
    }
}