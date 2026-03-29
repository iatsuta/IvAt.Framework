using SecuritySystem.ExternalSystem.Management;

namespace SecuritySystem.Services;

public interface IPrincipalDataSecurityIdentityManager
{
    TypedSecurityIdentity Extract(PrincipalData principalData);
}