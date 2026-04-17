using Microsoft.Extensions.DependencyInjection;
using SyncWorkflow.DependencyInjection;

namespace SyncWorkflow.Tests._Base;

public abstract class MultiScopeWorkflowTestBase
{
    private readonly Lazy<IServiceProvider> lazyRootServiceProvider;

    protected MultiScopeWorkflowTestBase()
    {
        this.lazyRootServiceProvider = new Lazy<IServiceProvider>(this.BuildServiceProvider);
    }

    protected IServiceProvider RootServiceProvider => this.lazyRootServiceProvider.Value;

    protected virtual IServiceCollection CreateServices()
    {
        return new ServiceCollection().RegisterSyncWorkflowBase();
    }

    private IServiceProvider BuildServiceProvider()
    {
        return this.CreateServices()
            .ValidateDuplicateDeclaration()
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
    }
}