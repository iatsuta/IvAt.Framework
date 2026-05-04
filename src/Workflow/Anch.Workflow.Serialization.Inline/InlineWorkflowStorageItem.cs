using Anch.GenericQueryable;
using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline;

public class InlineSpecificWorkflowExternalStorage<TSource>(
    IWorkflowDefinition workflowDefinition,
    IInstanceIdGenerator<WorkflowInstance> workflowInstanceIdGenerator,
    IInstanceIdGenerator<StateInstance> stateInstanceIdGenerator,
    IWorkflowInstanceSerializerFactory workflowInstanceSerializerFactory,
    IInlineStorage<TSource> persistSource)
    : IWorkflowRepository

    where TSource : notnull
{
    private readonly IWorkflowInstanceSerializer workflowInstanceSerializer = workflowInstanceSerializerFactory.Create(workflowDefinition);

    public IWorkflowDefinition WorkflowDefinition { get; } = workflowDefinition;

    public async ValueTask SaveWorkflowInstance(WorkflowInstance workflowInstance, CancellationToken cancellationToken)
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

        this.workflowInstanceSerializer.Serialize(workflowInstance);

        await persistSource.Save((TSource)workflowInstance.Source, cancellationToken);
    }

    public IAsyncEnumerable<WaitEventInfo> GetWaitEvents()
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<WaitEventInfo> GetWaitEvents(PushEventInfo pushEventInfo)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<WorkflowInstance> TryGetWorkflowInstance(WorkflowInstanceIdentity identity, CancellationToken cancellationToken)
    {
        var queryable = persistSource.GetQueryable();

        var source = queryable.Where(persistSource.GetFilter(identity.ToFull(this.WorkflowDefinition.Identity))).Single();

        var wi = this.workflowInstanceSerializer.Deserialize(source);

#if DEBUG
        if (identity != wi.Identity)
        {
            throw new InvalidOperationException();
        }
#endif

        return wi;
    }

    public async ValueTask<StateInstance> TryGetStateInstance(StateInstanceIdentity identity, CancellationToken cancellationToken)
    {
        var queryable = persistSource.GetQueryable();

        var source = queryable.Where(persistSource.GetFilter(identity.ToFull(this.WorkflowDefinition.Identity))).Single();

        var wi = this.workflowInstanceSerializer.Deserialize(source);

#if DEBUG
        if (identity != wi.CurrentState.Identity)
        {
            throw new InvalidOperationException();
        }
#endif

        return wi.CurrentState;
    }

    public IAsyncEnumerable<WorkflowInstance> GetWorkflowInstances()
    {
        var queryable = persistSource.GetQueryable();

        return queryable
            .GenericAsAsyncEnumerable()
            .Select(source => this.workflowInstanceSerializer.Deserialize(source!));
    }

    public async ValueTask FlushChanges(CancellationToken cancellationToken)
    {
        await persistSource.FlushChanges(cancellationToken);
    }
}