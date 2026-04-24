using ExampleApp.Infrastructure.DependencyInjection.UndirectView;

using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Infrastructure.Services;

public class EmptySchemaInitializer(
    AppDbContext dbContext,
    IEnumerable<IViewCreationScriptProvider> viewCreationScriptProviders) : IEmptySchemaInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        foreach (var createViewScript in viewCreationScriptProviders.SelectMany(v => v.GetScripts()))
        {
            await dbContext.Database.ExecuteSqlRawAsync(createViewScript, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}