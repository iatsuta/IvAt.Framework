namespace CommonFramework.Testing.Database;

public interface ITestConnectionStringProvider
{
    TestDatabaseConnectionString EmptySnapshot { get; }

    TestDatabaseConnectionString FilledSnapshot { get; }

    TestDatabaseConnectionString Actual { get; }
}