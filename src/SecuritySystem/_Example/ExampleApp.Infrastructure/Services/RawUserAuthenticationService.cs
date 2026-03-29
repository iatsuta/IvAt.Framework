using CommonFramework;

using Microsoft.AspNetCore.Http;

using SecuritySystem.Services;

namespace ExampleApp.Infrastructure.Services;

public class ExampleRawUserAuthenticationService(IUserCredentialNameResolver userCredentialNameResolver, IHttpContextAccessor httpContextAccessor, IDefaultCancellationTokenSource defaultCancellationTokenSource)
    : ImpersonateUserAuthenticationService(userCredentialNameResolver, defaultCancellationTokenSource)
{
    protected override string GetPureUserName() => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "system";
}