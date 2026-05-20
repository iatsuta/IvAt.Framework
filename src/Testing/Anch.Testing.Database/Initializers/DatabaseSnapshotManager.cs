using Anch.Testing.Database.ConnectionStringManagement;

namespace Anch.Testing.Database.Initializers;

public class DatabaseSnapshotManager(IDatabaseManager databaseManager) : IDatabaseSnapshotManager
{
    public ValueTask RestoreDatabaseSnapshot(ServiceProviderIndex serviceProviderIndex, CancellationToken ct) =>
        databaseManager.Copy(TestConnectionStringRole.FilledSnapshot, new PoolTestConnectionStringRole(serviceProviderIndex), ct);

    public ValueTask RemoveRestoredDatabase(ServiceProviderIndex serviceProviderIndex, CancellationToken ct) =>
        databaseManager.Remove(new PoolTestConnectionStringRole(serviceProviderIndex), ct);
}