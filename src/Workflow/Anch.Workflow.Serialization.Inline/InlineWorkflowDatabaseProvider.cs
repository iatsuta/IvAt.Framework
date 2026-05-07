using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Serialization.Inline;

public class InlineWorkflowDatabaseProvider : IWorkflowDatabaseProvider
{
    public void AddServices(IServiceCollection services) =>

        services.AddSingleton<IWorkflowRepositoryFactory, InlineWorkflowRepositoryFactory>();

    //    .AddScoped<IWorkflowInstanceSerializerFactory, WorkflowInstanceSerializerFactory>()
    //.AddScoped<IStateInstanceSerializerFactory, StateInstanceSerializerFactory>()
    //.AddSingleton<IStateDefinitionResolverFactory, StateDefinitionResolverFactory>()
}