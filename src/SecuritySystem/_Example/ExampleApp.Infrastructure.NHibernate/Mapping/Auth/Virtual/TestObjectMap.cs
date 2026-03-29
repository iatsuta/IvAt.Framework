using ExampleApp.Domain.Auth.Virtual;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping.Auth.Virtual;

public class TestManagerMap : ClassMap<TestManager>
{
    public TestManagerMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.Employee).Column(nameof(TestManager.Employee) + "Id").Not.Nullable();
        this.References(x => x.BusinessUnit).Column(nameof(TestManager.BusinessUnit) + "Id").Not.Nullable();
        this.References(x => x.Location).Column(nameof(TestManager.Location) + "Id").Not.Nullable();
    }
}