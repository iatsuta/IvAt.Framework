using System.Collections.Immutable;

namespace SecuritySystem.Services;

public class PermissionBindingInfoSource : IPermissionBindingInfoSource
{
    private readonly IReadOnlyDictionary<Type, PermissionBindingInfo> permissionDict;

    private readonly IReadOnlyDictionary<Type, ImmutableArray<PermissionBindingInfo>> principalDict;

    public PermissionBindingInfoSource(IEnumerable<PermissionBindingInfo> bindingInfoList)
    {
        var cache = bindingInfoList.ToList();

        this.principalDict = cache.GroupBy(v => v.PrincipalType).ToDictionary(g => g.Key, g => g.ToImmutableArray());
        this.permissionDict = cache.ToDictionary(v => v.PermissionType);
    }

    public PermissionBindingInfo GetForPermission(Type permissionType) => this.permissionDict[permissionType];

    public ImmutableArray<PermissionBindingInfo> GetForPrincipal(Type principalType) => this.principalDict[principalType];
}