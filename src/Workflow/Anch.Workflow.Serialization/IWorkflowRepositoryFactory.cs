using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

public interface IWorkflowRepositoryFactory
{
    public const string CacheKey = "Cache";

    IWorkflowRepository Create(WorkflowDefinitionIdentity workflowDefinitionIdentity);
}