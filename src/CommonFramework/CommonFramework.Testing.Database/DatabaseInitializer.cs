namespace CommonFramework.Testing.Database;

public class DatabaseSchemaInitializer(
    ISynchronizedInitializer<DatabaseSchemaInitializer> synchronizedInitializer,
    ITestConnectionStringProvider connectionStringProvider,
    IDatabaseChecker databaseChecker,
    IDatabaseSchemaCreator databaseSchemaCreator,
    IDatabaseCleaner databaseCleaner,
    TestDatabaseSettings settings) : ISchemaInitializer
{
    public async Task Initialize(CancellationToken cancellationToken) =>

        await synchronizedInitializer.Run(async () =>
        {
            if (!databaseChecker.Exists(connectionStringProvider.EmptySnapshot))
            {
                try
                {
                    await databaseSchemaCreator.Create(cancellationToken);
                }
                catch (Exception createSchemaEx)
                {
                    if (settings.RemoveDatabaseOnFailure)
                    {
                        try
                        {
                            await databaseCleaner.Clean(connectionStringProvider.EmptySnapshot, cancellationToken);
                        }
                        catch (Exception cleanEx)
                        {
                            throw new AggregateException(createSchemaEx, cleanEx);
                        }
                    }

                    throw;
                }
            }
        });
}