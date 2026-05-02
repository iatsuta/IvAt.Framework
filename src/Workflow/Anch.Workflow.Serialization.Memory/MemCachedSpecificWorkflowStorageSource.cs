using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Memory;

public class MemCachedSpecificWorkflowStorageSource(
    IWorkflowSource workflowSource,
    ISpecificWorkflowExternalStorageSource externalStorageSource)
    : SpecificWorkflowStorageSource(workflowSource)
{
    protected override ISpecificWorkflowStorage CreateSpecificWorkflowStorage(IWorkflowDefinition wfRef)
    {
        var externalStorage = externalStorageSource.GetSpecificStorageDict()[wfRef.Identity];

        return new MemCachedSpecificWorkflowStorage(externalStorage, wfRef);
    }
}