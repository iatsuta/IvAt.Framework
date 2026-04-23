namespace CommonFramework.Testing.Database;

public interface IDatabaseChecker
{
    bool Exists (TestDatabaseConnectionString connectionString);
}