namespace CommonFramework.Testing.Database;

public class RestoreBackupInitializer(
    ISynchronizedInitializer<RestoreBackupInitializer> synchronizedInitializer,
    IDatabaseChecker databaseChecker,
    ITestConnectionStringProvider connectionStringProvider) : IRestoreBackupInitializer
{
    public Task Initialize(CancellationToken cancellationToken) =>

        synchronizedInitializer.Run(async () =>
        {
            if (!databaseChecker.Exists(connectionStringProvider.Actual))
            {
                await emptyDatabaseSchemaSeeder.SeedTestData(cancellationToken);
            }
        });
}