using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization.Inline;

public class StateDefinitionResolver<TSource, TStatus>(IWorkflowDefinition workflow) : IStateDefinitionResolver<TSource>
    where TStatus : notnull
{
    private readonly WorkflowDomainBindingInfo<TSource, TStatus> workflowDomainBindingInfo =
        (WorkflowDomainBindingInfo<TSource, TStatus>)workflow.DomainBindingInfo;

    //private readonly Func<TSource, TStatus> getStatus = ((Expression<Func<TSource, TStatus>>)workflow.Definition.DomainBindingInfo.StatusProperty!).Compile(LambdaCompileCache.Default);

    private readonly Dictionary<TStatus, IStateDefinition> statusMap = workflow.States.Where(state => state.Status != null).ToDictionary(st => (TStatus)st.Status!);

    public IStateDefinition GetCurrentStateDefinition(TSource source)
    {
        return this.statusMap[this.workflowDomainBindingInfo.Status.Getter(source)];

        //var stateDefinition = workflow.Definition.States.Single(state => (TStatus)state.Status == this.statusExpr. );

        //throw new NotImplementedException();

        //var isTerminated = workflowInfo.IsTerminated;
        //var isFinished = workflowInfo.IsFinished;

        //var isTerminatedOrFinished = isTerminated || isFinished;

        //var currentStateDefinition =

        //    isTerminated ? this.workflow.Definition.TerminateState
        //    : isFinished ? this.workflow.Definition.FinalState
        //    : this.workflow.Definition.States.Single(s => s.Name == source.Status.ToString());
    }
}