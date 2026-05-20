namespace Anch.Testing;

public interface IServiceProviderPool : IAsyncDisposable
{
    ValueTask<IServiceProvider> GetAsync(CancellationToken ct);

    ValueTask ReleaseAsync(IServiceProvider serviceProvider, CancellationToken ct);
}