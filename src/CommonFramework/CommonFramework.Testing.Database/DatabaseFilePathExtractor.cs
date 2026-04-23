using System.Data.Common;

namespace CommonFramework.Testing.Database;

public class DatabaseFilePathExtractor : IDatabaseFilePathExtractor
{
    public string Extract(TestDatabaseConnectionString connectionString) =>
        new DbConnectionStringBuilder { ConnectionString = connectionString.Value }["Data Source"].ToString()!;
}