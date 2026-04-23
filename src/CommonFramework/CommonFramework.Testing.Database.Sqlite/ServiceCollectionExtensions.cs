using Microsoft.Extensions.DependencyInjection;

namespace CommonFramework.Testing.Database.Sqlite;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqliteTesting(this IServiceCollection services) =>
        services
            .AddSingleton<ITestConnectionStringProvider, TestConnectionStringProvider>()
            .AddKeyedSingleton<ITestEnvironmentHook, PrepareDatabaseEnvironmentHook>(EnvironmentHookType.Before)
            .AddKeyedSingleton<ITestEnvironmentHook, CleanDatabaseEnvironmentHook>(EnvironmentHookType.After)

            .AddSingleton<ISchemaInitializer, DatabaseSchemaInitializer>()
            .AddSingleton<IFillTestDataInitializer, FillTestDataInitializer>()
            .AddSingleton<IRestoreBackupInitializer, RestoreBackupInitializer>()

            .AddSingleton(typeof(ISynchronizedInitializer<>), typeof(SynchronizedInitializer<>))

            .AddSingleton<IDatabaseChecker, FileDatabaseChecker>()
            .AddSingleton<IDatabaseCleaner, FileDatabaseCleaner>()

            .AddSingleton<IDatabaseFilePathExtractor, SqliteDatabaseFilePathExtractor>()
            .AddSingleton<ITestDatabaseConnectionStringBuilder, SqliteTestDatabaseConnectionStringBuilder>();
}