using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Inline;

public interface IStateDefinitionResolverFactory
{
    IStateDefinitionResolver<TSource> Create<TSource>(IWorkflowDefinition workflowDefinition);
}