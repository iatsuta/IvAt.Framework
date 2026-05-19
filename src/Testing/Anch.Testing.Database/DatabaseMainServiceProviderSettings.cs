namespace Anch.Testing.Database;

public class DatabaseMainServiceProviderSettings(IParallelizationSettings parallelizationSettings, TestDatabaseSettings testDatabaseSettings)
    : IMainServiceProviderSettings
{
    public bool ReturnToPool { get; } = !parallelizationSettings.AllowParallelization || testDatabaseSettings.InitMode == DatabaseInitMode.External;
}