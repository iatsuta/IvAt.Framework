using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Infrastructure.Services;

public class DbSchemeInitializer(AppDbContext dbContext, IEnumerable<CreateViewSql> createViewSqlList) : IDbSchemeInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        foreach (var createViewSql in createViewSqlList)
        {
            await dbContext.Database.ExecuteSqlRawAsync(createViewSql.Text, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}