using System.Collections.Immutable;

using SecuritySystem.DiTests.Services;
using SecuritySystem.ExternalSystem;

namespace SecuritySystem.DiTests.Environment;

public class TestPermissionSource(TestPermissions data, DomainSecurityRule.ExpandedRoleGroupSecurityRule securityRule) : IPermissionSource
{
    public ValueTask<bool> HasAccessAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Dictionary<Type, Array>> GetPermissionsAsync(ImmutableArray<Type> securityContextTypes)
    {
        var roles = securityRule.Children.SelectMany(c => c.SecurityRoles).ToHashSet();

        return

            from permission in data.Permissions.ToAsyncEnumerable()

            where roles.Contains(permission.SecurityRole)

            select permission.Restrictions.ChangeValue(Array (idents) => idents.ToArray());
    }
}