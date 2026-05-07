using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Persistence.Inline;

public class StateInstanceSerializerFactory(IServiceProxyFactory serviceProxyFactory) : IStateInstanceSerializerFactory
{
    public IStateInstanceSerializer Create(IWorkflowDefinition workflow) =>
        serviceProxyFactory.Create<IStateInstanceSerializer>(typeof(StateInstanceSerializer<>).MakeGenericType(workflow.SourceType), workflow);
}