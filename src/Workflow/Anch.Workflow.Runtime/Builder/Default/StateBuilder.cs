using System.Linq.Expressions;

using Anch.Core;
using Anch.Workflow.Builder.Default.DomainDefinition;
using Anch.Workflow.Domain;
using Anch.Workflow.States;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.Builder.Default;

public class StateBuilder<TSource, TStatus, TState> : WorkflowBuilder<TSource, TStatus>, IStateBuilder<TSource, TStatus, TState>
    where TSource : notnull
    where TStatus : notnull
    where TState : IState
{
    public StateBuilder(WorkflowDefinitionBuilder<TSource, TStatus> workflowBuilder, bool addDoneEvent)
        : base(workflowBuilder)
    {
        this.StateDefinitionBuilder.Workflow = workflowBuilder;

        if (addDoneEvent)
        {
            this.StateDefinitionBuilder.Events.Add(new EventDefinitionBuilder
            {
                Header = EventHeader.StateDone
            });
        }

        workflowBuilder.AddState(this.StateDefinitionBuilder);
    }

    public StateDefinitionBuilder<TSource, TStatus, TState> StateDefinitionBuilder { get; } = new();

    public IStateBuilder<TSource, TStatus, TState> Input<TStateProperty, TSourceProperty, TService>(
        Expression<Func<TState, TStateProperty>> getStateProperty,
        Func<TSource, TService, CancellationToken, ValueTask<TSourceProperty>> getSourceProperty)
        where TSourceProperty : TStateProperty
        where TService : notnull

    {
        var setStatePropertyAction = getStateProperty.GetProperty().GetSetValueAction<TState, TStateProperty>();

        Func<IServiceProvider, TSource, TState, CancellationToken, ValueTask> setAction = async (serviceProvider, source, state, ct) =>

            setStatePropertyAction(state, await getSourceProperty(source, serviceProvider.GetRequiredService<TService>(), ct));

        this.StateDefinitionBuilder.InputActions.Add(setAction);

        return this;
    }

    public IStateBuilder<TSource, TStatus, TState> Output<TSourceProperty, TStateProperty, TService>(
        Expression<Func<TSource, TSourceProperty>> getSourceProperty,
        Func<TState, TService, CancellationToken, ValueTask<TStateProperty>> getStateProperty)
        where TStateProperty : TSourceProperty
        where TService : notnull
    {
        var setSourcePropertyAction = getSourceProperty.GetProperty().GetSetValueAction<TSource, TSourceProperty>();

        Func<IServiceProvider, TState, TSource, CancellationToken, ValueTask> setAction = async (serviceProvider, state, source, ct) =>

            setSourcePropertyAction(source, await getStateProperty(state, serviceProvider.GetRequiredService<TService>(), ct));

        this.StateDefinitionBuilder.OutputActions.Add(setAction);

        return this;
    }

    public IStateBuilder<TSource, TStatus, TState> WithName(string name)
    {
        this.StateDefinitionBuilder.Name = name;
        this.StateDefinitionBuilder.IsAutoName = false;

        return this;
    }

    public IStateBuilder<TSource, TStatus, TState> WithStatus(TStatus status)
    {
        this.StateDefinitionBuilder.Status = status;

        return this.WithName(status.ToString() ?? throw new ArgumentOutOfRangeException(nameof(status)));
    }

    IStateDefinitionBuilder<TSource, TStatus> IStateBuilder<TSource, TStatus>.StateDefinitionBuilder => this.StateDefinitionBuilder;
}