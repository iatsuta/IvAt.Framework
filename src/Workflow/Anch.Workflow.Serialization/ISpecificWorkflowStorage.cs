using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization;

public interface ISpecificWorkflowStorage : IWorkflowStorageBase<WorkflowInstanceIdentity, StateInstanceIdentity>
{
    IWorkflowDefinition WorkflowDefinition { get; }
}