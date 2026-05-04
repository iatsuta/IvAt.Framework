using Anch.Core.DictionaryCache;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

//public abstract class SpecificWorkflowRepositorySource : ISpecificWorkflowRepositorySource
//{
//    private readonly IDictionaryCache<WorkflowDefinitionIdentity, IWorkflowRepository> cache;

//    protected SpecificWorkflowRepositorySource(IWorkflowSource workflowSource) =>

//        this.cache = new DictionaryCache<WorkflowDefinitionIdentity, IWorkflowRepository>(wfRef =>
//            this.CreateSpecificWorkflowRepository(workflowSource.Workflows[wfRef]));

//    protected abstract IWorkflowRepository CreateSpecificWorkflowRepository(IWorkflowDefinition wfRef);

//    public IWorkflowRepository GetSpecificStorageDict(WorkflowDefinitionIdentity workflowDefinitionIdentity) =>
//        this.cache[workflowDefinitionIdentity];
//}