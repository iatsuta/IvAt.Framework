using CommonFramework.Testing.Database.ConnectionStringManagement;

using Microsoft.Extensions.DependencyInjection;

namespace CommonFramework.Testing.Database.Initializers;

public class CachedEmptySchemaInitializer(
    ISynchronizedInitializer<CachedEmptySchemaInitializer> synchronizedInitializer,
    ITestConnectionStringProvider connectionStringProvider,
    IDatabaseManager databaseManager,
    [FromKeyedServices(TestDatabaseInitializer.EmptySchemaKey)]
    IInitializer emptySchemaInitializer,
    TestDatabaseSettings settings) : IInitializer
{
    public Task Initialize(CancellationToken cancellationToken) =>

        synchronizedInitializer.Run(async () =>
        {
            if (!await databaseManager.Exists(connectionStringProvider.EmptySnapshot, cancellationToken))
            {
                try
                {
                    await emptySchemaInitializer.Initialize(cancellationToken);

                    await databaseManager.Copy(settings.DefaultConnectionString, connectionStringProvider.EmptySnapshot, false, cancellationToken);
                }
                catch (Exception createSchemaEx)
                {
                    if (settings.RemoveDatabaseOnFailure)
                    {
                        try
                        {
                            await databaseManager.Remove(connectionStringProvider.EmptySnapshot, cancellationToken);
                            await databaseManager.Remove(settings.DefaultConnectionString, cancellationToken);
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