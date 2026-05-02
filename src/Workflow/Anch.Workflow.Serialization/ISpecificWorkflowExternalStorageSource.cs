using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

public interface ISpecificWorkflowExternalStorageSource
{
    IReadOnlyDictionary<WorkflowDefinitionIdentity, ISpecificWorkflowExternalStorage> GetSpecificStorageDict();
}