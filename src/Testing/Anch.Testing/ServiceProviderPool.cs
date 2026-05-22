using Anch.Core;
using Anch.Threading;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Testing;

public class ServiceProviderPool(ITestEnvironment testEnvironment, bool? allowParallelization) : IServiceProviderPool
{
    private readonly AsyncLazy<IServiceProviderPool> lazyInternalServiceProviderPool = new(async ct =>
    {
        var serviceProviderBuildContext = ServiceProviderBuildContext.Main;

        var services = new ServiceCollection()
            .AddKeyedSingleton<IServiceProvider>(ITestEnvironment.MainServiceProviderKey, (sp, _) => sp)
            .AddSingleton(serviceProviderBuildContext.Index)
            .AddSingleton<IParallelizationSettings, ParallelizationSettings>();

        if (allowParallelization != null)
        {
            services.AddSingleton(new AllowParallelizationConstraint(allowParallelization.Value));
        }

        var mainServiceProvider = testEnvironment.BuildServiceProvider(services, serviceProviderBuildContext);

        foreach (var initializer in mainServiceProvider.GetKeyedServices<IInitializer>(ITestEnvironment.MainServiceProviderKey))
        {
            await initializer.Initialize(ct);
        }

        var mainServiceProviderSettings = mainServiceProvider.GetService<IMainServiceProviderSettings>();

        return new InternalServiceProviderPool(
            testEnvironment,
            mainServiceProvider,
            mainServiceProvider.GetRequiredService<IParallelizationSettings>(),
            mainServiceProviderSettings?.ReturnToPool ?? true);
    });

    public async ValueTask<IServiceProvider> GetAsync(CancellationToken ct)
    {
        var v = await this.lazyInternalServiceProviderPool.GetValueAsync(ct);

        return await v.GetAsync(ct);
    }

    public async ValueTask ReleaseAsync(IServiceProvider serviceProvider, CancellationToken ct)
    {
        var v = await this.lazyInternalServiceProviderPool.GetValueAsync(ct);

        await v.ReleaseAsync(serviceProvider, ct);
    }

    public ValueTask DisposeAsync() => this.lazyInternalServiceProviderPool.DisposeAsync();
}