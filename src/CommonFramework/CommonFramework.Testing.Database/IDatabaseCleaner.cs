namespace CommonFramework.Testing.Database;

public interface IDatabaseCleaner
{
    ValueTask Clean(TestDatabaseConnectionString connectionString, CancellationToken ct);
}