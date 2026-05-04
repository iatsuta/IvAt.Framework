using Anch.Workflow.States;

namespace Anch.Workflow.StateFactory;

public interface IStateFactory
{
    IState CreateState(IServiceProvider serviceProvider, object source);

    ValueTask BindInput(IState state, IServiceProvider serviceProvider, object source, CancellationToken ct);

    ValueTask BindOutput(IState state, IServiceProvider serviceProvider, object source, CancellationToken ct);
}