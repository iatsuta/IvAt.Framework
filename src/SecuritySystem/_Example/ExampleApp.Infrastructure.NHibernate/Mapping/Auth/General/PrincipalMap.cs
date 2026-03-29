using ExampleApp.Domain.Auth.General;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping.Auth.General;

public class PrincipalMap : ClassMap<Principal>
{
    public PrincipalMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.RunAs).Column(nameof(Principal.RunAs) + "Id");

        this.Map(x => x.Name).Not.Nullable();
    }
}