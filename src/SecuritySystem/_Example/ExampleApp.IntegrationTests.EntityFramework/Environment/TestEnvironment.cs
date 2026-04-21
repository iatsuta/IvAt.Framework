using ExampleApp.Infrastructure.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: CommonFramework.Testing.CommonTestFramework<ExampleApp.IntegrationTests.Environment.TestEnvironment>]

namespace ExampleApp.IntegrationTests.Environment;

public class TestEnvironment : TestEnvironmentBase
{
    protected override IServiceCollection InitializeServices(IServiceCollection services, IConfiguration configuration) =>
        services.AddEntityFrameworkInfrastructure(configuration);
}