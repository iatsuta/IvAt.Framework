using System.Collections.Immutable;

namespace SecuritySystem.Services;

public interface IPermissionBindingInfoSource
{
    PermissionBindingInfo GetForPermission(Type permissionType);

    ImmutableArray<PermissionBindingInfo> GetForPrincipal(Type principalType);
}