namespace CommonFramework.Testing.XunitEngine;

public interface IServiceProviderPool
{
    ValueTask<IServiceProvider> GetAsync(CancellationToken ct);

    ValueTask ReleaseAsync(IServiceProvider serviceProvider, CancellationToken ct);
}