namespace CommonFramework.Testing.Database;

public interface IDatabaseSchemaGenerator
{
    Task Generate(string connectionString, CancellationToken cancellationToken);
}