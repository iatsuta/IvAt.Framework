using CommonFramework;
using CommonFramework.DependencyInjection;
using CommonFramework.Testing;
using CommonFramework.Testing.Database;
using CommonFramework.Testing.Database.ConnectionStringManagement;
using CommonFramework.Testing.Database.Sqlite;

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

    protected abstract IServiceCollection AddServices(IServiceCollection services);

    public IServiceProvider BuildServiceProvider(IServiceCollection services) =>

        services

            .AddSingleton<IGenericQueryableSetupConfigurator, GenericQueryableSetupConfigurator>()

            .Pipe(this.AddServices)

            .AddSingleton(new TestDatabaseSettings { InitMode = this.databaseInitMode, DefaultConnectionString = new("Data Source=test.db;Mode=ReadWrite;") })
            .AddSingleton<IDatabaseSchemaCreator, DatabaseSchemaCreator>()
            .AddSingleton<EmptyDatabaseSchemaSeeder>()
            .AddSingletonFrom<IEmptyDatabaseSchemaSeeder, EmptyDatabaseSchemaSeeder>()


            .ReplaceSingletonFrom<IMainConnectionStringSource, ITestConnectionStringProvider>(provider => new MainConnectionStringSource(provider.Actual.Value))

            //IDatabaseSchemaCreator

            .AddSqliteTesting()

            .AddValidator<DuplicateServiceUsageValidator>()
            .Validate()
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
}