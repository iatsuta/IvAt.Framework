using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Serialization.Memory;

public class MemoryWorkflowDatabaseProvider : IWorkflowDatabaseProvider
{
    public void AddServices(IServiceCollection services) =>

        services
            .AddSingleton<MemoryWorkflowRootState>()
            .AddScoped<IWorkflowRepositoryFactory, MemoryWorkflowRepositoryFactory>();
}