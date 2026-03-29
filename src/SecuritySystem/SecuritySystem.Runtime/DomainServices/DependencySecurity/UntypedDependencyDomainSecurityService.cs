using CommonFramework;
using CommonFramework.IdentitySource;

using SecuritySystem.DomainServices.DependencySecurity._Base;
using SecuritySystem.Expanders;

using SecuritySystem.Providers;
using SecuritySystem.Providers.DependencySecurity;

namespace SecuritySystem.DomainServices.DependencySecurity;

public class UntypedDependencyDomainSecurityService<TDomainObject, TBaseDomainObject>(
    IServiceProxyFactory serviceProxyFactory,
    ISecurityRuleExpander securityRuleExpander,
    IDomainSecurityService<TBaseDomainObject> baseDomainSecurityService,
    IIdentityInfo<TDomainObject> domainIdentityInfo)
    : DependencyDomainSecurityServiceBase<TDomainObject, TBaseDomainObject>(
        securityRuleExpander,
        baseDomainSecurityService)
{
    protected override ISecurityProvider<TDomainObject> CreateDependencySecurityProvider(ISecurityProvider<TBaseDomainObject> baseProvider)
    {
        var securityProviderType = typeof(UntypedDependencySecurityProvider<,,>)
            .MakeGenericType(typeof(TDomainObject), typeof(TBaseDomainObject), domainIdentityInfo.IdentityType);

        return serviceProxyFactory.Create<ISecurityProvider<TDomainObject>>(securityProviderType, baseProvider);
    }
}