using Anch.Workflow.Definition;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Storage;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Engine;

public class WorkflowHost(IServiceProvider serviceProvider, IWorkflowStorage rootWorkflowStorage, IWorkflowMachineFactory workflowMachineFactory)
    : IWorkflowHost
{
    public IWorkflowExecutor CreateExecutor(WorkflowExecutionPolicy executionPolicy)
    {
        return new WorkflowExecutor(this, serviceProvider, rootWorkflowStorage, executionPolicy);
    }

    public IWorkflowMachine CreateMachine<TSource, TWorkflow>(TSource source)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>
    {
        return this.CreateMachine(source, serviceProvider.GetRequiredService<TWorkflow>());
    }

    public IWorkflowMachine CreateMachine<TSource, TWorkflow>(TSource source, TWorkflow workflow)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>
    {
        return this.CreateMachine(source, workflow.Definition);
    }

    public IWorkflowMachine CreateMachine(object source, IWorkflowDefinition workflowDefinition)
    {
        var wi = new WorkflowInstance
        {
            Definition = workflowDefinition,
            Source = source,
            Status = WorkflowStatus.NotStarted
        };

        var m = this.CreateMachine(wi);

        m.SetStartState();

        return m;
    }

    public IWorkflowMachine CreateMachine(WorkflowInstance workflowInstance)
    {
        return workflowMachineFactory.Create(workflowInstance);
    }
}