using Microsoft.Extensions.DependencyInjection;

namespace CommonFramework.Testing.XunitEngine;

public static class ServiceProviderExtensions
{
    public static async ValueTask RunEnvironmentHooks(this IServiceProvider serviceProvider, EnvironmentHookType hookType, CancellationToken ct)
    {
        foreach (var hook in serviceProvider.GetKeyedServices<ITestEnvironmentHook>(hookType))
        {
            await hook.Process(ct);
        }
    }
}