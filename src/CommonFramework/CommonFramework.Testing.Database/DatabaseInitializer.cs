namespace CommonFramework.Testing.Database;

public class DatabaseSchemaInitializer(
    ISynchronizedInitializer<DatabaseSchemaInitializer> synchronizedInitializer,
    ITestConnectionStringProvider connectionStringProvider,
    IDatabaseChecker databaseChecker,
    IDatabaseSchemaCreator databaseSchemaCreator) : IDatabaseSchemaInitializer
{
    public async Task Initialize(CancellationToken cancellationToken) =>

        await synchronizedInitializer.Run(async () =>
        {
            if (!databaseChecker.Exists(connectionStringProvider.EmptySnapshot))
            {
                await databaseSchemaCreator.Create(cancellationToken);
            }
        });
}