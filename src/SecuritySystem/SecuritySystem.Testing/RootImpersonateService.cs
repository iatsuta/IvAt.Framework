using SecuritySystem.Credential;
using SecuritySystem.Services;

namespace SecuritySystem.Testing;

public class RootImpersonateService(RootImpersonateServiceState state) : ImpersonateService, IRootImpersonateService
{
    protected override UserCredential? CustomUserCredential
    {
        get => state.CustomUserCredential;
        set => state.CustomUserCredential = value;
    }
}