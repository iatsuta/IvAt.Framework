using Anch.Core;
using Anch.Core.DictionaryCache;
using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Serialization;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Engine;

public class WorkflowMachineFactory : IWorkflowMachineFactory
{
    private readonly IDictionaryCache<WorkflowInstance, IWorkflowMachine> cache;

    public WorkflowMachineFactory(
        IServiceProxyFactory serviceProxyFactory,
        [FromKeyedServices(IWorkflowRepositoryFactory.CacheKey)]
        IWorkflowRepositoryFactory workflowRepositoryFactory) =>

        this.cache = new DictionaryCache<WorkflowInstance, IWorkflowMachine>(wi =>

            serviceProxyFactory.Create<IWorkflowMachine>(
                typeof(WorkflowMachine<>).MakeGenericType(wi.Definition.SourceType),
                workflowRepositoryFactory.Create(wi.Definition.Identity),
                wi));

    public IWorkflowMachine Create(WorkflowInstance workflowInstance) => this.cache[workflowInstance];

    public IWorkflowMachine Create<TSource>(TSource source, IWorkflowDefinition<TSource> workflowDefinition)
        where TSource : notnull
    {
        var wi = new WorkflowInstance
        {
            Definition = workflowDefinition,
            Source = source,
            Status = WorkflowStatus.NotStarted
        };

        var m = this.Create(wi);

        m.SetStartState();

        return m;
    }
}