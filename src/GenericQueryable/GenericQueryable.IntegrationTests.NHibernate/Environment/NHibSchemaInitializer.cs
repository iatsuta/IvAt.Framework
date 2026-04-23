using NHibernate.Tool.hbm2ddl;

namespace GenericQueryable.IntegrationTests.Environment;

public class NHibSchemaInitializer(global::NHibernate.Cfg.Configuration nhibConfiguration) : IDbSchemaInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        var schemaExport = new SchemaExport(nhibConfiguration);

        await schemaExport.CreateAsync(false, true, cancellationToken);
    }
}