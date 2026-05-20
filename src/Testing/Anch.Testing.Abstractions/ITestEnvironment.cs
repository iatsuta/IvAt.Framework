using Microsoft.Extensions.DependencyInjection;

namespace Anch.Testing;

public interface ITestEnvironment
{
    public const string MainServiceProviderKey = "ServiceProviderPool.Main";

    IServiceProvider BuildServiceProvider(IServiceCollection services, ServiceProviderBuildContext buildContext);
}