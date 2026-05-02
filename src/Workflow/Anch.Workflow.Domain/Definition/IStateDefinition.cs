using System.Collections.Frozen;
using System.Collections.Immutable;

namespace Anch.Workflow.Domain.Definition;

public interface IStateDefinition
{
    string Name { get; }

    object? Status { get; }

    Type StateType { get; set; }

    IWorkflowDefinition Workflow { get; }

    ImmutableList<Delegate> InputActions { get; }

    ImmutableList<Delegate> OutputActions { get; }

    ImmutableList<ITransitionDefinition> Transitions { get; }

    ImmutableList<IWorkflowDefinition> SubWorkflow { get; }

    FrozenDictionary<string, object> AdditionalInfo { get; }
}