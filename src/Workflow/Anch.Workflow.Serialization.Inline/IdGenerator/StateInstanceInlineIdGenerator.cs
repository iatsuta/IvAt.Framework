using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline.IdGenerator;

public class StateInstanceInlineIdGenerator() : InlineIdGenerator<StateInstance>(si => si.Workflow);