using Microsoft.Extensions.DependencyInjection;
using SyncWorkflow.DependencyInjection;
using SyncWorkflow.Domain.Runtime;
using SyncWorkflow.Engine;
using SyncWorkflow.Storage;

namespace SyncWorkflow.Tests._Base;

public abstract class SingleScopeWorkflowTestBase<TSource, TWorkflow> : MultiScopeWorkflowTestBase
    where TWorkflow : class, IWorkflow<TSource>, new()
{
    private readonly Lazy<IServiceScope> lazyScope;

    protected SingleScopeWorkflowTestBase()
    {
        this.lazyScope = new Lazy<IServiceScope>(() => this.RootServiceProvider.CreateScope());
    }

    protected IWorkflowHost Host => this.ScopeServiceProvider.GetWorkflowHost();

    protected IWorkflowStorage Storage => this.ScopeServiceProvider.GetWorkflowStorage();

    protected IServiceProvider ScopeServiceProvider => this.lazyScope.Value.ServiceProvider;

    protected async Task<WorkflowInstance> StartWorkflow(TSource source, CancellationToken cancellationToken = default)
    {
        return await this.Host.CreateExecutor(WorkflowExecutionPolicy.Full).StartWorkflow<TSource, TWorkflow>(source, cancellationToken);
    }


    protected override IServiceCollection CreateServices()
    {
        return base.CreateServices()

                   .RegisterSyncWorkflowType<TWorkflow>();
    }
}