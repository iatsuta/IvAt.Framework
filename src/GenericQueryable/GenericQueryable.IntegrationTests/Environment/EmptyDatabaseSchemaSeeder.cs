using CommonFramework.GenericRepository;
using CommonFramework.Testing.Database;

using GenericQueryable.IntegrationTests.Domain;

using Microsoft.Extensions.DependencyInjection;

namespace GenericQueryable.IntegrationTests.Environment;

public class EmptyDatabaseSchemaSeeder(IServiceProvider rootServiceProvider) : IEmptyDatabaseSchemaSeeder
{
    public async Task SeedTestData(CancellationToken cancellationToken)
    {
        await using var scope = rootServiceProvider.CreateAsyncScope();
        var serviceProvider = scope.ServiceProvider;
        var genericRepository = serviceProvider.GetRequiredService<IGenericRepository>();

        var fetchObj = new FetchObject();

        await genericRepository.SaveAsync(fetchObj, cancellationToken);
        await genericRepository.SaveAsync(new TestObject { Id = this.TestObjId, FetchObject = fetchObj }, cancellationToken);
    }

    public Guid TestObjId { get; } = Guid.NewGuid();
}