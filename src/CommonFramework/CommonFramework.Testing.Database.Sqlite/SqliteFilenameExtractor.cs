using System.Data.Common;

namespace CommonFramework.Testing.Database.Sqlite;

public class SqliteFilenameExtractor : ISqliteFilenameExtractor
{
    public string Extract(string connectionString) =>
        new DbConnectionStringBuilder { ConnectionString = connectionString }["Data Source"].ToString()!;
}