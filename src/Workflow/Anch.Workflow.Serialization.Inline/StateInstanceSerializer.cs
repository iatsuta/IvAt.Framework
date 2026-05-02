using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline;

public class StateInstanceSerializer<TSource>(
    IWorkflowDefinition workflowDefinition,
    IStateDefinitionResolverFactory stateDefinitionResolverFactory) : IStateInstanceSerializer
{
    private readonly IStateDefinitionResolver<TSource> stateDefinitionResolver = stateDefinitionResolverFactory.Create<TSource>(workflowDefinition);

    public StateInstance Deserialize(object source)
    {
        throw new NotImplementedException();
        //var currentStateDefinition = this.stateDefinitionResolver.GetCurrentStateDefinition(source);

        //var isFinished = currentStateDefinition.StateType == typeof(FinalState);
        //var isTerminated = currentStateDefinition.StateType == typeof(TerminateState);

        //var stateInstance = new StateInstance
        //{
        //    Id = source.Id,
        //    Workflow = result,
        //    Definition = currentStateDefinition,
        //    InputProcessed = true,
        //    OutputProcessed = isFinished || isTerminated
        //};
    }

    public void Serialize(StateInstance workflowInstance)
    {
        throw new NotImplementedException();
    }
}