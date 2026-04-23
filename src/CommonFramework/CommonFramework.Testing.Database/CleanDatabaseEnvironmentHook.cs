namespace CommonFramework.Testing.Database;

public class CleanDatabaseEnvironmentHook(
    IDatabaseCleaner databaseCleaner,
    ITestConnectionStringProvider connectionStringProvider) : ITestEnvironmentHook
{
    public ValueTask Process(CancellationToken ct) => databaseCleaner.Clean(connectionStringProvider.Actual, ct);
}