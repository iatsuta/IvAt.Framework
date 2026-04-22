using CommonFramework.Threading;

namespace CommonFramework.Testing.XunitEngine;

public class ServiceProviderSynchronizationContext(IAsyncLockerProvider asyncLockerProvider) : IServiceProviderSynchronizationContext
{
    public IAsyncLockerProvider AsyncLockerProvider { get; } = asyncLockerProvider;
}