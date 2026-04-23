namespace CommonFramework.Testing.Database;

public class FileDatabaseChecker(IDatabaseFilePathExtractor databaseFilePathExtractor) : IDatabaseChecker
{
    public bool Exists(TestDatabaseConnectionString connectionString) => File.Exists(databaseFilePathExtractor.Extract(connectionString));
}