using CommonFramework;

using ExampleApp.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

namespace ExampleApp.IntegrationTests;

public abstract class TestBase(IServiceProvider rootServiceProvider) : IAsyncLifetime
{
    protected IServiceProvider RootServiceProvider { get; } = rootServiceProvider;

    protected ITestingEvaluator<TService> GetEvaluator<TService>() => this.RootServiceProvider.GetRequiredService<ITestingEvaluator<TService>>();

    protected CancellationToken CancellationToken => TestContext.Current.CancellationToken;

    protected RootAuthManager AuthManager => this.RootServiceProvider.GetRequiredService<RootAuthManager>();

    public virtual async ValueTask InitializeAsync()
    {
        this.RootServiceProvider.GetRequiredService<RootImpersonateServiceState>().Reset();

        await this.RootServiceProvider.GetRequiredKeyedService<IInitializer>(RootAppInitializer.Key).Initialize(this.CancellationToken);
    }

    public virtual ValueTask DisposeAsync() => ValueTask.CompletedTask;
}