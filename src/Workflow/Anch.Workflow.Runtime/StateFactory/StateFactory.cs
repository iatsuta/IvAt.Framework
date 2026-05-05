using Anch.Core;
using Anch.Workflow.Domain.Definition;
using Anch.Workflow.States;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.StateFactory;

public class StateFactory<TState, TSource>(IStateDefinition stateDefinition) : IStateFactory
    where TState : IState
{
    private readonly List<Func<IServiceProvider, TSource, TState, CancellationToken, ValueTask>> inputActions =
        stateDefinition.InputActions.Cast<Func<IServiceProvider, TSource, TState, CancellationToken, ValueTask>>().ToList();

    private readonly List<Func<IServiceProvider, TState, TSource, CancellationToken, ValueTask>> outputActions =
        stateDefinition.OutputActions.Cast<Func<IServiceProvider, TState, TSource, CancellationToken, ValueTask>>().ToList();

    public IState CreateState(IServiceProvider serviceProvider, object source) =>
        serviceProvider.GetRequiredService<IServiceProxyFactory>().Create<TState>();

    public async ValueTask BindInput(IState state, IServiceProvider serviceProvider, object source, CancellationToken ct)
    {
        foreach (var initAction in this.inputActions)
        {
            await initAction(serviceProvider, (TSource)source, (TState)state, ct);
        }
    }

    public async ValueTask BindOutput(IState state, IServiceProvider serviceProvider, object source, CancellationToken ct)
    {
        foreach (var outputAction in this.outputActions)
        {
            await outputAction(serviceProvider, (TState)state, (TSource)source, ct);
        }
    }
}