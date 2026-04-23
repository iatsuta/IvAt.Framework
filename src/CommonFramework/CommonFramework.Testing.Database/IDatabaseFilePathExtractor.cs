namespace CommonFramework.Testing.Database;

public interface IDatabaseFilePathExtractor
{
    string Extract(TestDatabaseConnectionString connectionString);
}