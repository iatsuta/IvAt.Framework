namespace CommonFramework.Testing.Database.Initializers;

public class ExternalDatabaseInitializer : IInitializer
{
    public Task Initialize(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}