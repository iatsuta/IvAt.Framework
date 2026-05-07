using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Serialization;

public interface IWorkflowDatabaseProvider
{
    void AddServices(IServiceCollection services);
}