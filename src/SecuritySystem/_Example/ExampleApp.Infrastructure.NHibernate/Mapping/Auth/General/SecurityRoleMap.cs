using ExampleApp.Domain.Auth.General;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping.Auth.General;

public class SecurityRoleMap : ClassMap<SecurityRole>
{
    public SecurityRoleMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.Assigned();

        this.Map(x => x.Name).Not.Nullable();

        this.Map(x => x.Description).Not.Nullable();
    }
}