namespace CommonFramework.Testing.Database.Initializers;

public interface ISchemaSnapshotInitializer
{
    Task<string> Initialize(string defaultConnectionString, CancellationToken cancellationToken = default);
}