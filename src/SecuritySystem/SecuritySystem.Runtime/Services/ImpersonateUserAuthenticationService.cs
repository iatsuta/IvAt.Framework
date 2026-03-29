using CommonFramework;

using SecuritySystem.Credential;

namespace SecuritySystem.Services;

public abstract class ImpersonateUserAuthenticationService(
    IUserCredentialNameResolver userCredentialNameResolver,
    IDefaultCancellationTokenSource? defaultCancellationTokenSource)
    : ImpersonateService, IRawUserAuthenticationService
{
    public virtual string GetUserName() => this.CustomUserCredential switch
    {
        null => this.GetPureUserName(),

        UserCredential.NamedUserCredential namedUserCredential => namedUserCredential.Name,

        _ => defaultCancellationTokenSource.RunSync(ct => userCredentialNameResolver.GetUserNameAsync(this.CustomUserCredential, ct))
    };

    protected abstract string GetPureUserName();
}