using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Inline;

public class InlineSpecificWorkflowExternalStorageSource(IWorkflowSource workflowSource, IServiceProxyFactory serviceProxyFactory)
    : SpecificWorkflowExternalStorageSource(workflowSource)
{
    //
    protected override ISpecificWorkflowExternalStorage CreateSpecificWorkflowExternalStorage(IWorkflowDefinition wfRef)
    {
        var storageType = typeof(InlineSpecificWorkflowExternalStorage<>).MakeGenericType(wfRef.SourceType);

        return serviceProxyFactory.Create<ISpecificWorkflowExternalStorage>(storageType, wfRef);
    }
}