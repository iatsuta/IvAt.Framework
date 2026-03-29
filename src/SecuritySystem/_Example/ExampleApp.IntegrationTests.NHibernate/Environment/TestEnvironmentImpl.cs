using ExampleApp.Infrastructure.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleApp.IntegrationTests.Environment;

public class TestEnvironmentImpl : TestEnvironment
{
    protected override IServiceCollection InitializeServices(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddNHibernateInfrastructure(configuration)
            .AddSingleton(configuration);

    public static TestEnvironmentImpl Instance { get; } = new();
}