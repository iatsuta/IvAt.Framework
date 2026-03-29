using CommonFramework;

using ExampleApp.Application;

using SecuritySystem;
using SecuritySystem.DomainServices;

namespace ExampleApp.Infrastructure.Services;

public class RepositoryFactory<TDomainObject>(
    IServiceProxyFactory serviceProxyFactory,
    IDomainSecurityService<TDomainObject> domainSecurityService)
    : IRepositoryFactory<TDomainObject>
    where TDomainObject : class
{
    public IRepository<TDomainObject> Create(SecurityRule securityRule) =>
        serviceProxyFactory.Create<IRepository<TDomainObject>, Repository<TDomainObject>>(
            domainSecurityService.GetSecurityProvider(securityRule));
}