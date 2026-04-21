using Microsoft.Extensions.DependencyInjection;

namespace CommonFramework.Testing;

public interface ITestEnvironment
{
    IServiceProvider BuildServiceProvider(IServiceCollection services);

    ValueTask Initialize(IServiceProvider serviceProvider, CancellationToken ct) => ValueTask.CompletedTask;

    ValueTask Cleanup(IServiceProvider serviceProvider, CancellationToken ct) => ValueTask.CompletedTask;
}