using ExampleApp.Domain;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping;

public class BusinessUnitMap : ClassMap<BusinessUnit>
{
    public BusinessUnitMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.Parent).Column(nameof(BusinessUnit.Parent) + "Id");

        this.Map(x => x.Name).Not.Nullable();

        this.Map(x => x.DeepLevel).Not.Nullable();

        this.Map(x => x.AllowedForFilterRole).Not.Nullable();
    }
}