using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization;

public interface IWorkflowStorageBase<in TResolveWorkflowInstanceIdentity, in TResolveStateInstanceIdentity>
{
    ValueTask SaveWorkflowInstance(WorkflowInstance workflowInstance, CancellationToken cancellationToken = default);

    ValueTask<List<WaitEventInfo>> GetWaitEvents(PushEventInfo pushEventInfo, CancellationToken cancellationToken = default);


    ValueTask FlushChanges(CancellationToken cancellationToken = default);


    ValueTask<WorkflowInstance> GetWorkflowInstance(TResolveWorkflowInstanceIdentity identity, CancellationToken cancellationToken = default);

    ValueTask<StateInstance> GetStateInstance(TResolveStateInstanceIdentity identity, CancellationToken cancellationToken = default);


    ValueTask<List<WorkflowInstance>> GetWorkflowInstances(CancellationToken cancellationToken = default);

    ValueTask<List<WaitEventInfo>> GetWaitEvents(CancellationToken cancellationToken = default);
}