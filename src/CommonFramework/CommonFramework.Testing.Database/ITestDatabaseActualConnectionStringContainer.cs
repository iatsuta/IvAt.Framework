namespace CommonFramework.Testing.Database;

public interface ITestDatabaseActualConnectionStringSource
{
    TestDatabaseConnectionString ActualConnectionString { get; }
}