using SecuritySystem.Credential;

namespace SecuritySystem.Services;

public static class ImpersonateServiceExtensions
{
    public static Task WithImpersonateAsync(
        this IImpersonateService impersonateService,
        UserCredential customUserCredential,
        Func<Task> action) =>
        impersonateService.WithImpersonateAsync(
            customUserCredential,
            async () =>
            {
                await action();
                return default(object);
            });
}