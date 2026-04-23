namespace CommonFramework.Testing.Database;

public interface IDatabaseSchemaCreator
{
    Task Create(CancellationToken cancellationToken);
}