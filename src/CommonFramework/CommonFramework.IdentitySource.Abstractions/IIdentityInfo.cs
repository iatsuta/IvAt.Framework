using System.Linq.Expressions;

namespace CommonFramework.IdentitySource;

public interface IIdentityInfo<TDomainObject, TIdent> : IIdentityInfo<TDomainObject>
{
    PropertyAccessors<TDomainObject, TIdent> Id { get; }

    Expression<Func<TDomainObject, bool>> CreateFilter(IEnumerable<TIdent> idents);

    Expression<Func<TDomainObject, bool>> CreateFilter(TIdent ident);
}

public interface IIdentityInfo<in TDomainObject> : IIdentityInfo
{
    object GetId(TDomainObject domainObject);
}

public interface IIdentityInfo
{
    Type DomainObjectType { get; }

    Type IdentityType { get; }
}