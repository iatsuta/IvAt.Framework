using Anch.Workflow.Domain.Definition;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Persistence.Inline;

public class StateInstanceSerializer<TSource, TStatus>(
    IWorkflowDefinition<TSource, TStatus> workflowDefinition,
    IStateDefinitionResolverFactory stateDefinitionResolverFactory) : IStateInstanceSerializer<TSource>
    where TSource : class
    where TStatus : struct
{
    private readonly IStateDefinitionResolver<TSource, TStatus> stateDefinitionResolver = stateDefinitionResolverFactory.Create(workflowDefinition);

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