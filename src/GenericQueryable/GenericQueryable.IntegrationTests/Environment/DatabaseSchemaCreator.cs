using CommonFramework.Testing.Database;

namespace GenericQueryable.IntegrationTests.Environment;

public class DatabaseSchemaCreator(IDbSchemaInitializer schemaInitializer) : IDatabaseSchemaCreator
{
    public Task Create(CancellationToken cancellationToken) => schemaInitializer.Initialize(cancellationToken);
}