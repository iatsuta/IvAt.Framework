using ExampleApp.Domain.Auth.Virtual;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping.Auth.Virtual;

public class AdministratorMap : ClassMap<Administrator>
{
    public AdministratorMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.Employee).Column(nameof(Administrator.Employee) + "Id").Not.Nullable();
    }
}