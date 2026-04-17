using Microsoft.Extensions.DependencyInjection;
using SyncWorkflow.Engine;
using SyncWorkflow.Storage;

namespace SyncWorkflow.Tests._Base;

public static class ServiceProviderExtensions
{
    public static IWorkflowHost GetWorkflowHost(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IWorkflowHost>();
    }

    public static IWorkflowStorage GetWorkflowStorage(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IWorkflowStorage>();
    }
}