using CommonFramework.Testing.Database.ConnectionStringManagement;
using Microsoft.Extensions.DependencyInjection;

namespace CommonFramework.Testing.Database.Initializers;

public class CachingSharedTestDataInitializer(
    ISynchronizedInitializer<CachingSharedTestDataInitializer> synchronizedInitializer,
    ITestConnectionStringProvider connectionStringProvider,
    IDatabaseManager databaseManager,
    [FromKeyedServices(TestDatabaseInitializer.SharedTestDataKey)]
    IInitializer sharedTestDataInitializer,
    TestDatabaseSettings settings) : IInitializer
{
    public Task Initialize(CancellationToken cancellationToken) =>

        synchronizedInitializer.Run(async () =>
        {
            if (!await databaseManager.Exists(connectionStringProvider.FilledSnapshot, cancellationToken))
            {
                try
                {
                    await databaseManager.Copy(connectionStringProvider.EmptySnapshot, connectionStringProvider.FilledSnapshot, false, cancellationToken);

                    await sharedTestDataInitializer.Initialize(cancellationToken);
                }
                catch (Exception createSchemaEx)
                {
                    if (settings.RemoveDatabaseOnFailure)
                    {
                        try
                        {
                            await databaseManager.Remove(connectionStringProvider.FilledSnapshot, cancellationToken);
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
