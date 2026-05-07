using Anch.Workflow.Engine;
using Anch.Workflow.Serialization;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Tests._Base;

public static class ServiceProviderExtensions
{
    public static IWorkflowHost GetWorkflowHost(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IWorkflowHost>();
    }

    public static IWorkflowRepository GetRootRepository(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredKeyedService<IWorkflowRepository>(IWorkflowRepository.RootKey);
    }

    public static IWorkflowMachineFactory GetWorkflowMachineFactory(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IWorkflowMachineFactory>();
    }
}