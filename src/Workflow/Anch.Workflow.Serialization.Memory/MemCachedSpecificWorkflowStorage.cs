using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Memory;

public class MemCachedSpecificWorkflowStorage(ISpecificWorkflowExternalStorage externalStorage, IWorkflowDefinition workflowDefinition)
    : ISpecificWorkflowStorage
{
    private readonly Dictionary<WorkflowInstanceIdentity, WorkflowInstance> cache = new();

    public IWorkflowDefinition WorkflowDefinition { get; } = workflowDefinition;

    public virtual async ValueTask SaveWorkflowInstance(WorkflowInstance workflowInstance, CancellationToken cancellationToken = default)
    {
        await externalStorage.SaveWorkflowInstance(workflowInstance, cancellationToken);

        this.cache[workflowInstance.Identity] = workflowInstance;
    }

    public async ValueTask<WorkflowInstance> GetWorkflowInstance(WorkflowInstanceIdentity identity, CancellationToken cancellationToken = default)
    {
        if (this.cache.TryGetValue(identity, out var wi))
        {
            return wi;
        }
        else
        {
            return this.cache[identity] = await externalStorage.GetWorkflowInstance(identity, cancellationToken);
        }
    }

    public async ValueTask<StateInstance> GetStateInstance(StateInstanceIdentity identity, CancellationToken cancellationToken = default)
    {
        var cachedSi = this.cache.Values.Select(info => info.CurrentState).SingleOrDefault(currentSi => identity == currentSi.Identity);

        if (cachedSi == null)
        {
            var externalSi = await externalStorage.GetStateInstance(identity, cancellationToken);

            this.cache[externalSi.Workflow.Identity] = externalSi.Workflow;

            return externalSi;
        }
        else
        {
            return cachedSi;
        }
    }

    public async ValueTask<List<WorkflowInstance>> GetWorkflowInstances(CancellationToken cancellationToken = default)
    {
        return await externalStorage.GetWorkflowInstances(cancellationToken);
    }

    public async ValueTask<List<WaitEventInfo>> GetWaitEvents(CancellationToken cancellationToken = default)
    {
        return await externalStorage.GetWaitEvents(cancellationToken);
    }

    public async ValueTask FlushChanges(CancellationToken cancellationToken = default)
    {
        await externalStorage.FlushChanges(cancellationToken);
    }

    public async ValueTask<List<WaitEventInfo>> GetWaitEvents(PushEventInfo pushEventInfo, CancellationToken cancellationToken)
    {
        if (pushEventInfo.TargetState != null)
        {
            return pushEventInfo.TargetState.WaitEvents.Where(pushEventInfo.IsMatched).ToList();
        }
        else
        {
            return await externalStorage.GetWaitEvents(pushEventInfo, cancellationToken);
        }
    }
}