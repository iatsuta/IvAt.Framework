using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline;

public interface IWorkflowInstanceSerializer
{
    WorkflowInstance Deserialize(object source);

    void Serialize(WorkflowInstance workflowInstance);
}