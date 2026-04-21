namespace CommonFramework.Testing.Database;

public class InitializeSchemaTestEnvironmentHook(IT actualConnectionStringSource) : ITestEnvironmentHook
{
    public async ValueTask Process(CancellationToken ct)
    {
        await databaseInitializer.InitializeSchemaAsync(ct);
    }
}