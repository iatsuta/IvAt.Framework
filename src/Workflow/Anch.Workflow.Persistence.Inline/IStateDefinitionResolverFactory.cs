using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Persistence.Inline;

public interface IStateDefinitionResolverFactory
{
    IStateDefinitionResolver<TSource> Create<TSource>(IWorkflowDefinition workflowDefinition);
}