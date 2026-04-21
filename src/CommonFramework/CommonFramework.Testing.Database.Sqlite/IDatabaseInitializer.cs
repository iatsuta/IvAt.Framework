using System.Data.Common;

namespace CommonFramework.Testing.Database.Sqlite;

public class SqliteDatabaseInitializer : IDatabaseInitializer
{
    private readonly string dbName =
        new DbConnectionStringBuilder
            { ConnectionString = configuration.GetConnectionString("DefaultConnection") }["Data Source"].ToString()!;

    public Task InitializeSchemaAsync(CancellationToken cancellationToken)
    {
        if (File.Exists(this.dbName))
        {
            File.Delete(this.dbName);
        }
    }
}

public class 