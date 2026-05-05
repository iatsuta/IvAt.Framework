using Anch.Core;
using Anch.Workflow.Domain.Definition;
using Anch.Workflow.States;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.StateFactory;

public class StateFactory<TSource, TStatus, TState>(IStateDefinition<TSource, TStatus, TState> stateDefinition) : IStateFactory
    where TSource : notnull
    where TState : IState
{
    public IState CreateState(IServiceProvider serviceProvider, object source) =>
        serviceProvider.GetRequiredService<IServiceProxyFactory>().Create<TState>();

    public async ValueTask BindInput(IState state, IServiceProvider serviceProvider, object source, CancellationToken ct)
    {
        foreach (var initAction in stateDefinition.InputActions)
        {
            await initAction(serviceProvider, (TSource)source, (TState)state, ct);
        }
    }

    public async ValueTask BindOutput(IState state, IServiceProvider serviceProvider, object source, CancellationToken ct)
    {
        foreach (var outputAction in stateDefinition.OutputActions)
        {
            await outputAction(serviceProvider, (TState)state, (TSource)source, ct);
        }
    }
}