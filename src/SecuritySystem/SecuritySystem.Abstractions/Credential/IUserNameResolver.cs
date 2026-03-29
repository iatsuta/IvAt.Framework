namespace SecuritySystem.Credential;

public interface IUserNameResolver<out TUser>
{
    ValueTask<string?> ResolveAsync(SecurityRuleCredential credential, CancellationToken cancellationToken = default);
}
