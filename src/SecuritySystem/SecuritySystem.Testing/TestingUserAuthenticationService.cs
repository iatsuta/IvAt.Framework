using CommonFramework;

using SecuritySystem.Credential;
using SecuritySystem.Services;

namespace SecuritySystem.Testing;

public class TestingUserAuthenticationService(
    TestRootUserInfo testRootUserInfo,
    RootImpersonateServiceState rootImpersonateServiceState,
    IUserCredentialNameResolver userCredentialNameResolver,
    IDefaultCancellationTokenSource? defaultCancellationTokenSource = null)
    : ImpersonateUserAuthenticationService(userCredentialNameResolver, defaultCancellationTokenSource)
{
    protected override UserCredential? CustomUserCredential => rootImpersonateServiceState.CustomUserCredential ?? base.CustomUserCredential;

    protected override string GetPureUserName() => testRootUserInfo.Name;

    public override string GetUserName() =>
        this.CustomUserCredential == null ? base.GetUserName() : rootImpersonateServiceState.Cache.GetOrAdd(this.CustomUserCredential, _ => base.GetUserName());
}