using CommonFramework.Testing.XunitEngine;

namespace CommonFramework.Testing.Database.Sqlite;

public class SqliteDatabaseInitializer(
    IServiceProviderSynchronizationContext serviceProviderSynchronizationContext,
    ITestConnectionStringProvider testConnectionStringProvider) : IDatabaseSchemaInitializer
{
    private bool initialized = true;

    public async Task Initialize(CancellationToken cancellationToken = default)
    {
        if (!this.initialized)
        {
            lock (serviceProviderSynchronizationContext.Lock)
            {
                if (!this.initialized)
                {
                    testConnectionStringProvider.CreateWithPostfix("_Empty")
                }
            }

            this.initialized = true;
        }
    }
}

public interface ITestConnectionStringProvider
{
    string CreateWithPostfix();
}