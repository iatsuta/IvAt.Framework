using ExampleApp.Domain;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping;

public class ManagementUnitUndirectAncestorLinkMap : ClassMap<ManagementUnitUndirectAncestorLink>
{
    public ManagementUnitUndirectAncestorLinkMap()
    {
        this.Schema("app");

        this.SchemaAction.None();

        this.Id(x => x.Id).GeneratedBy.Assigned();

        this.References(x => x.Source).Column(nameof(ManagementUnitUndirectAncestorLink.Source) + "Id").Not.Nullable();
        this.References(x => x.Target).Column(nameof(ManagementUnitUndirectAncestorLink.Target) + "Id").Not.Nullable();
    }
}