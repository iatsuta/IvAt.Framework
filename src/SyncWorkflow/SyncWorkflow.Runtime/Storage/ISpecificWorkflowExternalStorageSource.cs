using SyncWorkflow.Definition;

namespace SyncWorkflow.Storage;

public interface ISpecificWorkflowExternalStorageSource
{
    IReadOnlyDictionary<WorkflowDefinitionIdentity, ISpecificWorkflowExternalStorage> GetSpecificStorageDict();
}