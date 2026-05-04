using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Engine;

public record ExecutionContext : IExecutionContext
{
    public required StateInstance StateInstance { get; init; }

    public required CancellationToken CancellationToken { get; init; }

    public required WaitEventInfo? CallbackEventInfo { get; init; }


    public object Source => this.WorkflowInstance.Source;

    public WorkflowInstance WorkflowInstance => this.StateInstance.Workflow;

    public bool IsActual => this.StateInstance.IsActual;
}