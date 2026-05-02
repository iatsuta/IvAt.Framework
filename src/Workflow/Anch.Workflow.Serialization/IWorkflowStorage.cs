using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization;

public interface IWorkflowStorage : IWorkflowStorageBase<WorkflowInstanceFullIdentity, StateInstanceFullIdentity>
{
    ISpecificWorkflowStorage GetSpecificStorage(WorkflowDefinitionIdentity identity);
}