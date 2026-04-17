using SyncWorkflow.Builder;
using SyncWorkflow.Builder.Default;

namespace SyncWorkflow.Tests.Sum;

public class SumWorkflow : BuildWorkflow<SumWorkflowObject>
{
    protected override void Build(IWorkflowBuilder<SumWorkflowObject> builder)
    {
        builder.Then(obj => obj.Result = obj.Value1 + obj.Value2);
    }
}