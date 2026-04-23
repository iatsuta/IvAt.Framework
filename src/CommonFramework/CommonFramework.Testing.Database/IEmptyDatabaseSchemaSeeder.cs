namespace CommonFramework.Testing.Database;

public interface IEmptyDatabaseSchemaSeeder
{
    Task SeedTestData(CancellationToken cancellationToken);
}