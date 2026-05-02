using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Builder.Default.DomainDefinition;

public class EventDefinition : IEventDefinition
{
    public EventHeader Header { get; set; } = null!;
}