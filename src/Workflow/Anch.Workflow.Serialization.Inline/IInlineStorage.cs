using System.Linq.Expressions;

using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline;

public interface IInlineStorage<TSource>
{
    ValueTask Save(TSource source, CancellationToken cancellationToken = default);

    ValueTask FlushChanges(CancellationToken cancellationToken = default);

    IQueryable<TSource> GetQueryable(CancellationToken cancellationToken = default);

    Expression<Func<TSource, bool>> GetFilter(WorkflowInstanceFullIdentity wi);

    Expression<Func<TSource, bool>> GetFilter(StateInstanceFullIdentity si);
}