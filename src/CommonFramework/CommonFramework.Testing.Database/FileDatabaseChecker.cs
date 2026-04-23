namespace CommonFramework.Testing.Database;

public class FileDatabaseChecker(IDatabaseFilePathExtractor databaseFilePathExtractor) : IDatabaseChecker
{
    public bool Exists(TestDatabaseConnectionString connectionString)
    {
        var filePath = databaseFilePathExtractor.Extract(connectionString);

        return File.Exists(filePath) && new FileInfo(filePath).Length > 0;
    }
}