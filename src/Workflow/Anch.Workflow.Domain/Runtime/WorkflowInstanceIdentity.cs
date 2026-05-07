using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Domain.Runtime;

public record WorkflowInstanceIdentity(Guid Id, WorkflowDefinitionIdentity? Definition = null);