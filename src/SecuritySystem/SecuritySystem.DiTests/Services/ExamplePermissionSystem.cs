using SecuritySystem.Expanders;
using SecuritySystem.ExternalSystem;

namespace SecuritySystem.DiTests.Services;

public class TestPermissionSystem(ISecurityRuleExpander securityRuleExpander, TestPermissions data) : IPermissionSystem
{
    public Type PermissionType => throw new NotImplementedException();

    public IEnumerable<IPermissionSource> GetPermissionSources(DomainSecurityRule.RoleBaseSecurityRule securityRule) =>
        [new TestPermissionSystem(data, securityRuleExpander.FullRoleExpand(securityRule))];

    public IAsyncEnumerable<SecurityRole> GetAvailableSecurityRoles() => data.Permissions.Select(p => p.SecurityRole).ToAsyncEnumerable();
}