using System.Collections.Immutable;

using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Execution;

public static class WorkflowProcessResultExtensions
{
    public static WorkflowProcessResult Aggregate(this IEnumerable<WorkflowProcessResult> workflowProcessResults)
    {
        var modifiedBuilder = ImmutableList.CreateBuilder<WorkflowInstance>();
        var unprocessedBuilder = ImmutableList.CreateBuilder<UnprocessedStateResult>();

        foreach (var workflowProcessResult in workflowProcessResults)
        {
            foreach (var otherModified in workflowProcessResult.Modified)
            {
                if (!modifiedBuilder.Contains(otherModified))
                {
                    modifiedBuilder.Add(otherModified);
                }
            }

            unprocessedBuilder.AddRange(workflowProcessResult.Unprocessed);
        }

        return new WorkflowProcessResult(modifiedBuilder.ToImmutable(), unprocessedBuilder.ToImmutable());
    }

    public static async Task<WorkflowProcessResult> Aggregate(this IEnumerable<Task<WorkflowProcessResult>> getResults)
    {
        var modifiedBuilder = ImmutableList.CreateBuilder<WorkflowInstance>();
        var unprocessedBuilder = ImmutableList.CreateBuilder<UnprocessedStateResult>();

        foreach (var getResult in getResults)
        {
            var workflowProcessResult = await getResult;

            foreach (var otherModified in workflowProcessResult.Modified)
            {
                if (!modifiedBuilder.Contains(otherModified))
                {
                    modifiedBuilder.Add(otherModified);
                }
            }

            unprocessedBuilder.AddRange(workflowProcessResult.Unprocessed);
        }

        return new WorkflowProcessResult(modifiedBuilder.ToImmutable(), unprocessedBuilder.ToImmutable());
    }
}