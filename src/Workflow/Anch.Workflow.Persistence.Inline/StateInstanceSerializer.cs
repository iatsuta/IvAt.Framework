using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Persistence.Inline;

public class StateInstanceSerializer<TSource, TStatus>(
    IWorkflowDefinition workflowDefinition,
    IStateDefinitionResolverFactory stateDefinitionResolverFactory) : IStateInstanceSerializer<TSource>
{
    private readonly IStateDefinitionResolver<TSource> stateDefinitionResolver = stateDefinitionResolverFactory.Create<TSource>(workflowDefinition);

    public StateInstance Deserialize(TSource source)
    {
        var currentStateDefinition = this.stateDefinitionResolver.GetCurrentStateDefinition(source);

        throw new NotImplementedException();

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