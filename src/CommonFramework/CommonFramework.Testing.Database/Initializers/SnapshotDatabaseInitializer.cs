using System.Data.Common;

namespace CommonFramework.Testing.Database.Initializers;

public class SnapshotDatabaseInitializer(TestDatabaseSettings testDatabaseSettings) : IInitializer
{
    private string GetFileName(string connectionString) =>
        new DbConnectionStringBuilder { ConnectionString = connectionString }["Data Source"].ToString()!;

    public Task Initialize(CancellationToken cancellationToken = default)
    {
        var dbName = this.GetFileName(testDatabaseSettings.DefaultConnectionString);

        if (!File.Exists(dbName))
        {
            Task<string> GenerateSchemeAsync(CancellationToken cancellationToken);
        }
    }
}

public interface ISchemeGenerator
{
    Task<string> GenerateSchemeAsync(CancellationToken cancellationToken);
}