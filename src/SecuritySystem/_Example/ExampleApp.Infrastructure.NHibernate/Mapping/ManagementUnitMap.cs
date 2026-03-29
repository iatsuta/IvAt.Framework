using ExampleApp.Domain;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping;

public class ManagementUnitMap : ClassMap<ManagementUnit>
{
    public ManagementUnitMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.Parent).Column(nameof(ManagementUnit.Parent) + "Id");

        this.Map(x => x.Name).Not.Nullable();

        this.Map(x => x.DeepLevel).Not.Nullable();
    }
}