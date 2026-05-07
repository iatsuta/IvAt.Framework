namespace Anch.Workflow.Execution;

public static class WorkflowProcessResultExtensions
{
    public static WorkflowProcessResult Aggregate(this IEnumerable<WorkflowProcessResult> workflowProcessResults) =>
        workflowProcessResults.Aggregate(WorkflowProcessResult.Empty, (acc, result) => acc + result);

    public static ValueTask<WorkflowProcessResult> AggregateAsync<T>(
        this IEnumerable<T> source,
        Func<T, CancellationToken, ValueTask<WorkflowProcessResult>> selector,
        CancellationToken cancellationToken) =>
        source
            .ToAsyncEnumerable()
            .AggregateAsync(WorkflowProcessResult.Empty, async (state, item, ct) => state + await selector(item, ct), cancellationToken);
}