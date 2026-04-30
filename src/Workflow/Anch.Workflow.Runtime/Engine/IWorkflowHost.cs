using Anch.Workflow.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Engine;

public interface IWorkflowHost
{
    IWorkflowExecutor CreateExecutor(WorkflowExecutionPolicy executionPolicy);

    IWorkflowMachine CreateMachine<TSource, TWorkflow>(TSource source)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>;

    IWorkflowMachine CreateMachine<TSource, TWorkflow>(TSource source, TWorkflow workflow)
        where TSource : notnull
        where TWorkflow : IWorkflow<TSource>;

    IWorkflowMachine CreateMachine(object source, IWorkflowDefinition workflowDefinition);

    IWorkflowMachine CreateMachine(WorkflowInstance workflowInstance);
}