using System.Linq.Expressions;

using NHibernate;

using SyncWorkflow.Domain.Runtime;
using SyncWorkflow.Storage.Inline;

namespace SyncWorkflow.NHibernate;

public class NHibInlineStorage<TSource>(ISession session) : IInlineStorage<TSource>
{
    public async Task Save(TSource source, CancellationToken cancellationToken = default)
    {
        await session.SaveAsync(source, cancellationToken);
    }

    public async Task FlushChanges(CancellationToken cancellationToken = default)
    {
        await session.FlushAsync(cancellationToken);
    }

    public IQueryable<TSource> GetQueryable(CancellationToken cancellationToken = default)
    {
        return session.Query<TSource>();
    }

    public Expression<Func<TSource, bool>> GetFilter(WorkflowInstanceFullIdentity wi)
    {
        return wfObj => wfObj.Id == wi.Id;
    }

    public Expression<Func<TSource, bool>> GetFilter(StateInstanceFullIdentity si)
    {
        return wfObj => wfObj.Id == si.Id;
    }
}