using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Engine;

public interface IWorkflowMachineFactory
{
    IWorkflowMachine Create(WorkflowInstance wi);

    IWorkflowMachine Create<TSource>(TSource source, IWorkflow<TSource> workflow)
        where TSource : notnull;

    IWorkflowMachine Create(object source, IWorkflowDefinition workflowDefinition);
}