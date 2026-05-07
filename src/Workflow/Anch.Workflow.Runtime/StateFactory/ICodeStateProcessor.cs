using Anch.Workflow.States;

namespace Anch.Workflow.StateFactory;

public interface ICodeStateProcessor
{
    IState CodeState { get; }

    void SetStatus();

    ValueTask BindInput(CancellationToken ct);

    ValueTask BindOutput(CancellationToken ct);
}