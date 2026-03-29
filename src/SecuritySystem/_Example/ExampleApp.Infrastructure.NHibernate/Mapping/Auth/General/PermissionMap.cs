using ExampleApp.Domain.Auth.General;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping.Auth.General;

public class PermissionMap : ClassMap<Permission>
{
    public PermissionMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.References(x => x.Principal).Column(nameof(Permission.Principal) + "Id").Not.Nullable();
        this.References(x => x.SecurityRole).Column(nameof(Permission.SecurityRole) + "Id").Not.Nullable();

        this.References(x => x.DelegatedFrom).Column(nameof(Permission.DelegatedFrom) + "Id");


        this.Map(x => x.StartDate);
        this.Map(x => x.EndDate);

        this.Map(x => x.Comment).Not.Nullable();
        this.Map(x => x.ExtendedValue).Not.Nullable();
    }
}