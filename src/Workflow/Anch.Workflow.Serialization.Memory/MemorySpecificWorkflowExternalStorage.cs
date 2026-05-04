using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Memory;

public class MemorySpecificWorkflowExternalStorage(
    IWorkflowDefinition workflowDefinition,
    IInstanceIdGenerator<WorkflowInstance> workflowInstanceIdGenerator,
    IInstanceIdGenerator<StateInstance> stateInstanceIdGenerator)
    : ISpecificWorkflowExternalStorage
{
    private readonly Dictionary<WorkflowInstanceIdentity, WorkflowInstance> Values = new();

    public IWorkflowDefinition WorkflowDefinition { get; } = workflowDefinition;

    public async ValueTask SaveWorkflowInstance(WorkflowInstance workflowInstance, CancellationToken cancellationToken = default)
    {
        if (workflowInstance.Definition != this.WorkflowDefinition)
        {
            throw new InvalidOperationException("Wrong storage");
        }

        if (workflowInstance.Id == Guid.Empty)
        {
            workflowInstance.Id = workflowInstanceIdGenerator.GenerateId(workflowInstance);
        }

        var currentState = workflowInstance.CurrentState;

        if (currentState.Id == Guid.Empty)
        {
            currentState.Id = stateInstanceIdGenerator.GenerateId(currentState);
        }

        this.Values[workflowInstance.Identity] = workflowInstance;
    }

    public async ValueTask<List<WaitEventInfo>> ReleaseWaitEvents(PushEventInfo pushEventInfo, CancellationToken cancellationToken = default)
    {
        var waitEvents = await this.GetWaitEvents(pushEventInfo, cancellationToken);

        foreach (var waitEventInfo in waitEvents)
        {
            waitEventInfo.Release();
        }

        return waitEvents;
    }

    public async ValueTask<WorkflowInstance> GetWorkflowInstance(WorkflowInstanceIdentity identity, CancellationToken cancellationToken = default)
    {
        return this.Values[identity];
    }

    public async ValueTask<StateInstance> GetStateInstance(StateInstanceIdentity identity, CancellationToken cancellationToken = default)
    {
        return this.Values
            .Values
            .Select(wi => wi.CurrentState)
            .Single(si => si.Identity == identity);
    }

    public async ValueTask<List<WorkflowInstance>> GetWorkflowInstances(CancellationToken cancellationToken = default)
    {
        return this.Values.Values.ToList();
    }

    public async ValueTask<List<WaitEventInfo>> GetWaitEvents(CancellationToken cancellationToken = default)
    {
        return this.Values.Values.SelectMany(wi => wi.CurrentState.WaitEvents).ToList();
    }

    public async ValueTask<List<WaitEventInfo>> GetWaitEvents(PushEventInfo pushEventInfo, CancellationToken cancellationToken = default)
    {
        var request =

            from wi in this.Values.Values

            from waitEventInfo in wi.CurrentState.WaitEvents

            where pushEventInfo.IsMatched(waitEventInfo)

            select waitEventInfo;

        return request.ToList();
    }

    public async ValueTask FlushChanges(CancellationToken cancellationToken = default)
    {
    }
}