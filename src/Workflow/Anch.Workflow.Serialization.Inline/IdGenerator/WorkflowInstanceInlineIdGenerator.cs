using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline.IdGenerator;

public class WorkflowInstanceInlineIdGenerator() : InlineIdGenerator<WorkflowInstance>(wi => wi);