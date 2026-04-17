using SyncWorkflow.Domain.Runtime;

namespace SyncWorkflow.Engine;

public interface IWorkflowMachineFactory
{
    IWorkflowMachine Create(WorkflowInstance wi);
}