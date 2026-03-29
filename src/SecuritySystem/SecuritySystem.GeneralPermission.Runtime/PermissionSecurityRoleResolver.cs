using SecuritySystem.Services;

namespace SecuritySystem.GeneralPermission;
public class PermissionSecurityRoleResolver<TPermission, TSecurityRole>(
    GeneralPermissionBindingInfo<TPermission, TSecurityRole> generalBindingInfo,
    ISecurityIdentityManager<TSecurityRole> securityRoleIdentityManager,
    ISecurityRoleSource securityRoleSource) : IPermissionSecurityRoleResolver<TPermission>
{
    public FullSecurityRole Resolve(TPermission permission)
    {
        var dbSecurityRole = generalBindingInfo.SecurityRole.Getter(permission);

        return securityRoleSource.GetSecurityRole(securityRoleIdentityManager.GetIdentity(dbSecurityRole));
    }
}