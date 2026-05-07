using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Domain.Runtime;

public record StateInstanceIdentity(Guid Id, WorkflowDefinitionIdentity? Definition = null);