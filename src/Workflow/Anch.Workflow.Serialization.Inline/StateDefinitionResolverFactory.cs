using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Inline;

public class StateDefinitionResolverFactory(IServiceProxyFactory serviceProxyFactory) : IStateDefinitionResolverFactory
{
    public IStateDefinitionResolver<TSource> Create<TSource>(IWorkflowDefinition workflow)
    {
        var statusType = workflow.DomainBindingInfo.StatusType!;

        var stateDefinitionResolverType = typeof(StateDefinitionResolver<,>).MakeGenericType(typeof(TSource), statusType);

        return serviceProxyFactory.Create<IStateDefinitionResolver<TSource>>(stateDefinitionResolverType, workflow);
    }
}