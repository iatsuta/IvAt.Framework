using Microsoft.Extensions.DependencyInjection;

namespace CommonFramework.Testing;

public interface ICommonTestFrameworkInitializer
{
    IServiceProvider BuildServiceProvider(IServiceCollection serviceCollection);
}