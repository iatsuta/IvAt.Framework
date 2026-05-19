namespace Anch.Testing;

public interface IServiceProviderPool : IAsyncDisposable
{
    public const string MainServiceProviderKey = "ServiceProviderPool.Main";

    ValueTask<IServiceProvider> GetAsync(CancellationToken ct);

    ValueTask ReleaseAsync(IServiceProvider serviceProvider, CancellationToken ct);
}