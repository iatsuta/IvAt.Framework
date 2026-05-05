using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Builder.Default.DomainDefinition;

public class TransitionDefinitionBuilder<TSource, TStatus> ://ITransitionDefinitionBuilder<TSource>,
    ITransitionDefinition<TSource, TStatus>
    where TSource : notnull
{
    public EventDefinitionBuilder Event { get; set; } = null!;

    public IStateDefinitionBuilder<TSource, TStatus> To { get; set; } = null!;


    IEventDefinition ITransitionDefinition.Event => this.Event;


    IStateDefinition ITransitionDefinition.To => this.To;

    //IStateDefinition<TSource, TStatus> ITransitionDefinition<TSource, TStatus>.To => this.To;

    //IStateDefinitionBuilder ITransitionDefinitionBuilder.To => this.To;

    IStateDefinition<TSource, TStatus> ITransitionDefinition<TSource, TStatus>.To => this.To;
}