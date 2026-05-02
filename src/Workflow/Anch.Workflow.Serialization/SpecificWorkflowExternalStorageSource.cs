using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

public abstract class SpecificWorkflowExternalStorageSource : ISpecificWorkflowExternalStorageSource
{
    private readonly Lazy<Dictionary<WorkflowDefinitionIdentity, ISpecificWorkflowExternalStorage>> lazyStorageDict;

    protected SpecificWorkflowExternalStorageSource(IWorkflowSource workflowSource)
    {
        this.lazyStorageDict = LazyHelper.Create(() =>
            workflowSource.GetWorkflows().ChangeValue(this.CreateSpecificWorkflowExternalStorage));
    }

    protected abstract ISpecificWorkflowExternalStorage CreateSpecificWorkflowExternalStorage(IWorkflowDefinition wfRef);

    public IReadOnlyDictionary<WorkflowDefinitionIdentity, ISpecificWorkflowExternalStorage> GetSpecificStorageDict()
    {
        return this.lazyStorageDict.Value;
    }
}

public class SpecificWorkflowExternalStorageSource<TSpecificWorkflowExternalStorage>(
    IServiceProxyFactory serviceProxyFactory,
    IWorkflowSource workflowSource)
    : SpecificWorkflowExternalStorageSource(workflowSource)
    where TSpecificWorkflowExternalStorage : ISpecificWorkflowExternalStorage
{
    protected override ISpecificWorkflowExternalStorage CreateSpecificWorkflowExternalStorage(IWorkflowDefinition wfRef)
    {
        return serviceProxyFactory.Create<ISpecificWorkflowExternalStorage, TSpecificWorkflowExternalStorage>(wfRef);
    }
}