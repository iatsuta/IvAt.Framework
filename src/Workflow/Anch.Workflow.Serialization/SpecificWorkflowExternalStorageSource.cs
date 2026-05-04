using Anch.Core;
using Anch.Core.DictionaryCache;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

//public abstract class SpecificWorkflowExternalStorageSource : ISpecificWorkflowExternalStorageSource
//{
//    private readonly IDictionaryCache<WorkflowDefinitionIdentity, IWorkflowRepository> cache;

//    protected SpecificWorkflowExternalStorageSource(IWorkflowSource workflowSource) =>

//        this.cache = new DictionaryCache<WorkflowDefinitionIdentity, IWorkflowRepository>(wfRef =>
//            this.CreateForDefinition(workflowSource.Workflows[wfRef]));

//    protected abstract IWorkflowRepository CreateForDefinition(IWorkflowDefinition wfRef);

//    public IWorkflowRepository GetForDefinition(WorkflowDefinitionIdentity workflowDefinitionIdentity) =>
//        this.cache[workflowDefinitionIdentity];
//}

//public class SpecificWorkflowExternalStorageSource<TSpecificWorkflowExternalStorage>(
//    IServiceProxyFactory serviceProxyFactory,
//    IWorkflowSource workflowSource)
//    : SpecificWorkflowExternalStorageSource(workflowSource)
//    where TSpecificWorkflowExternalStorage : IWorkflowRepository
//{
//    protected override IWorkflowRepository CreateForDefinition(IWorkflowDefinition wfRef) =>

//        serviceProxyFactory.Create<IWorkflowRepository, TSpecificWorkflowExternalStorage>(wfRef);
//}