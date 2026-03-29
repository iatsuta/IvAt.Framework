using SecuritySystem.Credential;

namespace SecuritySystem.Services;

public abstract class ImpersonateService : IImpersonateService
{
    protected virtual UserCredential? CustomUserCredential { get; set; }

    public async Task<T> WithImpersonateAsync<T>(UserCredential customUserCredential, Func<Task<T>> func)
    {
        var prev = this.CustomUserCredential;

        this.CustomUserCredential = customUserCredential;

        try
        {
            return await func();
        }
        finally
        {
            this.CustomUserCredential = prev;
        }
    }
}