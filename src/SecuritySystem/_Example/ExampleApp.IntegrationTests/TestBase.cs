using CommonFramework;

using ExampleApp.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

namespace ExampleApp.IntegrationTests;

public abstract class TestBase(IServiceProvider rootServiceProvider) : IAsyncLifetime
{
    protected ITestingEvaluator<TService> GetEvaluator<TService>() => rootServiceProvider.GetRequiredService<ITestingEvaluator<TService>>();

    protected RootAuthManager AuthManager => field ??= rootServiceProvider.GetRequiredService<RootAuthManager>();

    protected virtual async ValueTask InitializeAsync(CancellationToken ct)
    {
        rootServiceProvider.GetRequiredService<RootImpersonateServiceState>().Reset();

        await rootServiceProvider.GetRequiredKeyedService<IInitializer>(RootAppInitializer.Key).Initialize(ct);
    }

    ValueTask IAsyncLifetime.InitializeAsync() => this.InitializeAsync(TestContext.Current.CancellationToken);

    ValueTask IAsyncDisposable.DisposeAsync() => ValueTask.CompletedTask;
}