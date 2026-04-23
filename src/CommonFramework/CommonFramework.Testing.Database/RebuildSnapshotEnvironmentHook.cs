namespace CommonFramework.Testing.Database;

public class PrepareDatabaseEnvironmentHook(
    ISynchronizedInitializer<PrepareDatabaseEnvironmentHook> synchronizedInitializer,
    ISchemaInitializer databaseSchemaInitializer,
    IFillTestDataInitializer fillTestDataInitializer,
    IRestoreBackupInitializer restoreBackupInitializer) : ITestEnvironmentHook
{
    public ValueTask Process(CancellationToken ct) =>

        synchronizedInitializer.Run(async () =>
        {
            await databaseSchemaInitializer.Initialize(ct);

            await fillTestDataInitializer.Initialize(ct);

            await restoreBackupInitializer.Initialize(ct);
        });
}