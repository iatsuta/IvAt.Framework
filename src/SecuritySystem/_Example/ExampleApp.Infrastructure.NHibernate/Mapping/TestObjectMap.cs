using ExampleApp.Domain;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping;

public class TestObjectMap : ClassMap<TestObject>
{
    public TestObjectMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.BusinessUnit).Column(nameof(TestObject.BusinessUnit) + "Id").Not.Nullable();
        this.References(x => x.ManagementUnit).Column(nameof(TestObject.ManagementUnit) + "Id").Not.Nullable();
        this.References(x => x.Location).Column(nameof(TestObject.Location) + "Id").Not.Nullable();
    }
}