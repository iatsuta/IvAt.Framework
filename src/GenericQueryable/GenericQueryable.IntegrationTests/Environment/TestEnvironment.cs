using CommonFramework;
using CommonFramework.DependencyInjection;
using CommonFramework.Testing;
using CommonFramework.Testing.Database;

using Microsoft.Extensions.DependencyInjection;

namespace GenericQueryable.IntegrationTests.Environment;

public abstract class TestEnvironment : ITestEnvironment
{
    private readonly DatabaseInitMode databaseInitMode =

#if DEBUG
        DatabaseInitMode.ReuseSnapshot;
#else
        DatabaseInitMode.RebuildSnapshot;
#endif

    protected virtual IServiceCollection AddServices(IServiceCollection services)
    {
        return services;
    }

    public IServiceProvider BuildServiceProvider(IServiceCollection services) =>

        services

            .AddSingleton<IGenericQueryableSetupConfigurator, GenericQueryableSetupConfigurator>()

            .Pipe(this.AddServices)

            .AddSingleton(new TestDatabaseSettings(this.databaseInitMode, new TestDatabaseConnectionString("Data Source=test.db")))
            .ReplaceSingletonFrom<IMainConnectionStringSource, ITestConnectionStringProvider>(provider =>
                new MainConnectionStringSource(provider.ActualConnectionString.Value))

            .AddValidator<DuplicateServiceUsageValidator>()
            .Validate()
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
}