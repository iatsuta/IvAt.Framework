namespace CommonFramework.Testing.Database;

public class FileDatabaseCleaner(IDatabaseFilePathExtractor databaseFilePathExtractor) : IDatabaseCleaner
{

    public ValueTask Clean(TestDatabaseConnectionString connectionString, CancellationToken ct)
    {
        var filePath = databaseFilePathExtractor.Extract(connectionString);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return ValueTask.CompletedTask;
    }
}