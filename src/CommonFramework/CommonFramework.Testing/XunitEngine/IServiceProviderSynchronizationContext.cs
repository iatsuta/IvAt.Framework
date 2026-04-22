using CommonFramework.Threading;

namespace CommonFramework.Testing.XunitEngine;

public interface IServiceProviderSynchronizationContext
{
    IAsyncLockerProvider AsyncLockerProvider { get; }
}