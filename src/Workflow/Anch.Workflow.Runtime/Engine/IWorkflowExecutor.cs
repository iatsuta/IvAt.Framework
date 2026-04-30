using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Engine;

public interface IWorkflowExecutor
{
    Task<WorkflowProcessResult> StartWorkflow<TSource, TWorkflow>(TSource source, CancellationToken cancellationToken = default)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>;

    Task<WorkflowProcessResult> StartWorkflow<TSource, TWorkflow>(TSource source, TWorkflow workflow, CancellationToken cancellationToken = default)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>;

    async Task<WorkflowProcessResult> PushEvent(EventHeader @event, StateInstance targetState, object? data = null, CancellationToken cancellationToken = default)
    {
        return await this.PushEvent(new PushEventInfo(@event, null, targetState, data), cancellationToken);
    }

    Task<WorkflowProcessResult> PushEvent(PushEventInfo pushEventInfo, CancellationToken cancellationToken = default);
}