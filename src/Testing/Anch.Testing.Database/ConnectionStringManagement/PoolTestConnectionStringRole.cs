namespace Anch.Testing.Database.ConnectionStringManagement;

public record PoolTestConnectionStringRole(ServiceProviderIndex ServiceProviderIndex) : TestConnectionStringRole("Actual")
{
    public static PoolTestConnectionStringRole Main { get; } = new (ServiceProviderIndex.Main);
}