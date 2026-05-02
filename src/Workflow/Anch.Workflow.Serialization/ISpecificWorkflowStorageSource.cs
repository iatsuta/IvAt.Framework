using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

public interface ISpecificWorkflowStorageSource
{
    IReadOnlyDictionary<WorkflowDefinitionIdentity, ISpecificWorkflowStorage> GetSpecificStorageDict();
}