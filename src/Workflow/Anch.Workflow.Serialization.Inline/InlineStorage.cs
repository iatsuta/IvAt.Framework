using System.Linq.Expressions;

using Anch.GenericRepository;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline;

public class InlineStorage<TSource>(IGenericRepository genericRepository, IQueryableSource queryableSource) : IInlineStorage<TSource>
    where TSource : class
{
    public async ValueTask Save(TSource source, CancellationToken cancellationToken) => await genericRepository.SaveAsync(source, cancellationToken);

    public IQueryable<TSource> GetQueryable() => queryableSource.GetQueryable<TSource>();

    public Expression<Func<TSource, bool>> GetFilter(WorkflowInstanceIdentity wi)
    {
        throw new NotImplementedException();
    }

    public Expression<Func<TSource, bool>> GetFilter(StateInstanceIdentity si)
    {
        throw new NotImplementedException();
    }
}