namespace Anch.Testing;

public static class ServiceProviderPoolExtensions
{
    public static async ValueTask<ServiceProviderPoolScope> CreateScopeAsync(this IServiceProviderPool? serviceProviderPool, bool runHooks,
        CancellationToken ct)
    {
        if (serviceProviderPool == null)
        {
            return new ServiceProviderPoolScope(null, null, null, false, ct);
        }
        else
        {
            try
            {
                var serviceProvider = await serviceProviderPool.GetAsync(ct);

                if (runHooks)
                {
                    await serviceProvider.RunEnvironmentHooks(EnvironmentHookType.Before, ct);
                }

                return new ServiceProviderPoolScope(serviceProviderPool, serviceProvider, null, runHooks, ct);
            }
            catch (Exception ex)
            {
                return new ServiceProviderPoolScope(null, null, ex, false, ct);
            }
        }
    }

    public class ServiceProviderPoolScope(
        IServiceProviderPool? serviceProviderPool,
        IServiceProvider? serviceProvider,
        Exception? exception,
        bool runHooks,
        CancellationToken ct) : IAsyncDisposable
    {
        public Exception? Exception { get; } = exception;

        public IServiceProvider? ServiceProvider { get; } = serviceProvider;

        public async ValueTask DisposeAsync()
        {
            if (serviceProviderPool != null && this.ServiceProvider != null)
            {
                if (runHooks)
                {
                    await this.ServiceProvider.RunEnvironmentHooks(EnvironmentHookType.After, ct);
                }

                await serviceProviderPool.ReleaseAsync(this.ServiceProvider, ct);
            }
        }
    }
}