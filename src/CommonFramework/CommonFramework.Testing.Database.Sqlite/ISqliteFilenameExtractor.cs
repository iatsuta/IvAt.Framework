namespace CommonFramework.Testing.Database.Sqlite;

public interface ISqliteFilenameExtractor
{
    string Extract(string connectionString);
}