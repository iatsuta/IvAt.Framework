using ExampleApp.Domain;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping;

public class ManagementUnitDirectAncestorLinkMap : ClassMap<ManagementUnitDirectAncestorLink>
{
    public ManagementUnitDirectAncestorLinkMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.Ancestor).Column(nameof(ManagementUnitDirectAncestorLink.Ancestor) + "Id").Not.Nullable();
        this.References(x => x.Child).Column(nameof(ManagementUnitDirectAncestorLink.Child) + "Id").Not.Nullable();
    }
}