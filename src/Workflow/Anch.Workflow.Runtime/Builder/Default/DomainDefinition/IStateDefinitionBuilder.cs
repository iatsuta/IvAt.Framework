using Anch.Workflow.Domain.Definition;


namespace Anch.Workflow.Builder.Default.DomainDefinition;

public interface IStateDefinitionBuilder<TSource, TStatus> : IStateDefinition<TSource, TStatus>, IStateDefinitionBuilder
    where TSource : notnull
{
    new string Name { get; set; }

    bool IsAutoName { get; set; }

    List<EventDefinitionBuilder> Events { get; set; }

    new WorkflowDefinitionBuilder<TSource, TStatus> Workflow { get; set; }

    new List<TransitionDefinitionBuilder<TSource, TStatus>> Transitions { get; set; }

    //IWorkflowDefinitionBuilder<TSource> IStateDefinitionBuilder<TSource>.Workflow => this.Workflow;

    //IReadOnlyList<ITransitionDefinitionBuilder<TSource>> IStateDefinitionBuilder<TSource>.Transitions => this.Transitions;
}

//public interface IStateDefinitionBuilder<TSource> : IStateDefinitionBuilder, IStateDefinition<TSource>
//    where TSource : notnull
//{
//    new IWorkflowDefinitionBuilder<TSource> Workflow { get; }

//    new IReadOnlyList<ITransitionDefinitionBuilder<TSource>> Transitions { get; }


//    IWorkflowDefinition<TSource> IStateDefinition<TSource>.Workflow => this.Workflow;

//    IReadOnlyList<ITransitionDefinition<TSource>> IStateDefinition<TSource>.Transitions => this.Transitions;
//}

public interface IStateDefinitionBuilder : IStateDefinition
{
    new IReadOnlyList<IWorkflowDefinitionBuilder> SubWorkflows { get; }

    new IWorkflowDefinitionBuilder Workflow { get; }
}