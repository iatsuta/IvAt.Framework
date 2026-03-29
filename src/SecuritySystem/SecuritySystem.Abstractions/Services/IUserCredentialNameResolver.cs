using SecuritySystem.Credential;

namespace SecuritySystem.Services;

public interface IUserCredentialNameResolver
{
    ValueTask<string> GetUserNameAsync(UserCredential userCredential, CancellationToken cancellationToken = default);
}