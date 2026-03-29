using ExampleApp.Domain.Auth.General;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping.Auth.General;

public class PermissionRestrictionMap : ClassMap<PermissionRestriction>
{
    public PermissionRestrictionMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.SecurityContextType).Column(nameof(PermissionRestriction.SecurityContextType) + "Id").Not.Nullable();
        this.References(x => x.Permission).Column(nameof(PermissionRestriction.Permission) + "Id").Not.Nullable();

        this.Map(x => x.SecurityContextId);
    }
}