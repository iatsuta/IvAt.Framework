namespace Anch.Workflow.Domain.Definition;

public interface ITransitionDefinition
{
    IEventDefinition Event { get; }

    IStateDefinition To { get; }
}