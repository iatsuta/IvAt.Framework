namespace CommonFramework.Testing.Database;

public interface IDatabaseSchemaInitializer : IInitializer;


public interface IExternalDatabaseGenerator
{
    Task GenerateSchema(CancellationToken cancellationToken);

    Task FillTestData(CancellationToken cancellationToken);
}