using ExampleApp.Application;

namespace ExampleApp.Infrastructure.Services;

public interface IDal<TDomainObject> : IRepository<TDomainObject>;