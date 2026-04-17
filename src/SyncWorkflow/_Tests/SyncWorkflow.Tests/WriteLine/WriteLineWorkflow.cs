using SyncWorkflow.Builder;
using SyncWorkflow.Builder.Default;

namespace SyncWorkflow.Tests.WriteLine;

public class WriteLineWorkflow : BuildWorkflow<object>
{
    public const string Message = "Hello world!";

    protected override void Build(IWorkflowBuilder<object> builder)
    {
        builder.WriteLine(Message);
    }
}