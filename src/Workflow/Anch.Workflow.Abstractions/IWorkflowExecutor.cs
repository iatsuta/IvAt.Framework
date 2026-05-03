using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Execution;

namespace Anch.Workflow;

public interface IWorkflowExecutor
{
    Task<WorkflowProcessResult> Start<TSource, TWorkflow>(TSource source, CancellationToken cancellationToken = default)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>;

    Task<WorkflowProcessResult> Start<TSource>(TSource source, IWorkflow<TSource> workflow, CancellationToken cancellationToken = default)
        where TSource : notnull;

    Task<WorkflowProcessResult> ProcessUnprocessed(WorkflowProcessResult workflowProcessResult, CancellationToken cancellationToken = default);

    async Task<WorkflowProcessResult> PushEvent(EventHeader @event, StateInstance targetState, object? data = null,
        CancellationToken cancellationToken = default) =>

        await this.PushEvent(new PushEventInfo(@event, null, targetState, data), cancellationToken);

    Task<WorkflowProcessResult> PushEvent(PushEventInfo pushEventInfo, CancellationToken cancellationToken = default);
}