using Anch.Workflow.Domain;

namespace Anch.Workflow.Builder;

public interface ITaskBuilder<TSource, TStatus>
    where TSource : notnull
{
    ITaskBuilder<TSource, TStatus> AddCommand(EventHeader eventHeader, Action<IWorkflowBuilder<TSource, TStatus>> branchSetup);
}