using System.Linq.Expressions;

using CommonFramework;

namespace SecuritySystem.Services;

public interface IPermissionTypedRestrictionBindingInfo<TPermission>
{
    Expression<Func<TPermission, IQueryable<TSecurityContext>>> GetRestrictionsPath<TSecurityContext>()
        where TSecurityContext : class, ISecurityContext;

    Expression<Func<TPermission, bool>> GetUnrestrictedFilter<TSecurityContext>()
        where TSecurityContext : class, ISecurityContext => this.GetRestrictionsPath<TSecurityContext>().Select(v => !v.Any());
}