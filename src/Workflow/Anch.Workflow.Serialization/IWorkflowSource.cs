using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

public interface IWorkflowSource
{
    IReadOnlyDictionary<WorkflowDefinitionIdentity, IWorkflowDefinition> GetWorkflows();
}