using System.Collections.Concurrent;

using SecuritySystem.Credential;

namespace SecuritySystem.Testing;

public class RootImpersonateServiceState
{
    public UserCredential? CustomUserCredential { get; set; }

    public ConcurrentDictionary<UserCredential, string> Cache { get; set; } = [];

    public void Reset()
    {
        this.CustomUserCredential = null;
        this.Cache = [];
    }
}