using SyncWorkflow.Builder;
using SyncWorkflow.Builder.Default;
using SyncWorkflow.Tests.Wait;

namespace SyncWorkflow.Tests.StartWorkflow;

public class RootWorkflow : BuildWorkflow<WaitWorkflowSource>
{
    protected override void Build(IWorkflowBuilder<WaitWorkflowSource> builder)
    {
        builder.StartWorkflow<WaitWorkflowSource, WaitWorkflow>(v => v);
    }
}