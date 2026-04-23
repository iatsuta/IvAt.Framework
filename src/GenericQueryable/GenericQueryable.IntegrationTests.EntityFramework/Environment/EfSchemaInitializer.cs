namespace GenericQueryable.IntegrationTests.Environment;

public class EfSchemaInitializer(TestDbContext dbContext) : IDbSchemaInitializer
{
    public async Task Initialize(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}