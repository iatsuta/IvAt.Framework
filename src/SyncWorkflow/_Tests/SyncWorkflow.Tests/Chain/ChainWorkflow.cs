using SyncWorkflow.Builder;
using SyncWorkflow.Builder.Default;

namespace SyncWorkflow.Tests.Chain;

public class ChainWorkflow : BuildWorkflow<ChainWorkflowObject>
{
    protected override void Build(IWorkflowBuilder<ChainWorkflowObject> builder)
    {
        builder
            .Then(obj => obj.Result += obj.Value1)
            .Then(obj => obj.Result += obj.Value2);
    }
}