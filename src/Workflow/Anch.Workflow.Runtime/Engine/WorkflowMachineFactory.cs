using Anch.Core;
using Anch.Core.DictionaryCache;
using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Serialization;

namespace Anch.Workflow.Engine;

public class WorkflowMachineFactory : IWorkflowMachineFactory
{
    private readonly IDictionaryCache<WorkflowInstance, IWorkflowMachine> cache;

    public WorkflowMachineFactory(IServiceProxyFactory serviceProxyFactory, IWorkflowStorage workflowStorage)
    {
        this.cache = new DictionaryCache<WorkflowInstance, IWorkflowMachine>(wi =>

            serviceProxyFactory.Create<IWorkflowMachine>(
                typeof(WorkflowMachine<>).MakeGenericType(wi.Definition.SourceType),
                workflowStorage.GetSpecificStorage(wi.Definition.Identity),
                wi));
    }

    public IWorkflowMachine Create(WorkflowInstance workflowInstance)
    {
        return this.cache[workflowInstance];
    }

    public IWorkflowMachine Create<TSource>(TSource source, IWorkflow<TSource> workflow)
        where TSource : notnull
    {
        return this.Create(source, workflow.Definition);
    }

    public IWorkflowMachine Create(object source, IWorkflowDefinition workflowDefinition)
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