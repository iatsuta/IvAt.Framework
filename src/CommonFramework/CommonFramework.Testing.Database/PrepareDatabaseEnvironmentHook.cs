namespace CommonFramework.Testing.Database;

public class PrepareDatabaseEnvironmentHook(
    IDatabaseSchemaInitializer databaseSchemaInitializer,
    IDatabaseTestDataInitializer databaseTestDataInitializer) : ITestEnvironmentHook
{
    public async ValueTask Process(CancellationToken ct)
    {
        await databaseSchemaInitializer.Initialize(ct);

        await databaseTestDataInitializer.Initialize(ct);
    }
}