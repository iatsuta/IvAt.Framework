using CommonFramework.Testing.XunitEngine;
using CommonFramework.Threading;

namespace CommonFramework.Testing.Database;

public class SqliteDatabaseInitializer(
    IServiceProviderSynchronizationContext serviceProviderSynchronizationContext,
    ITestConnectionStringProvider testConnectionStringProvider,
    IDatabaseSchemaGenerator databaseSchemaGenerator) : IDatabaseSchemaInitializer
{
    private readonly IAsyncLocker asyncLocker =
        serviceProviderSynchronizationContext.AsyncLockerProvider.CreateLocker(typeof(SqliteDatabaseInitializer));

    private bool initialized = false;

    public async Task Initialize(CancellationToken cancellationToken = default)
    {
        if (!this.initialized)
        {
            using (await asyncLocker.CreateScope())
            {
                if (!this.initialized)
                {
                    var schemaConnectionString = testConnectionStringProvider.CreateWithPostfix("_Empty");

                    await databaseSchemaGenerator.Generate(schemaConnectionString, cancellationToken);
                }
            }

            this.initialized = true;
        }
    }
}