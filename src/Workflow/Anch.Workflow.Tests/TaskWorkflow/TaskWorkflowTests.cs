using Anch.Testing.Xunit;
using Anch.Workflow.Engine;
using Anch.Workflow.Tests._Base;

namespace Anch.Workflow.Tests.TaskWorkflow;

public class TaskWorkflowTests : SingleScopeWorkflowTestBase<TaskWorkflowObject, TaskWorkflow>
{
    [AnchFact]
    public async Task Task_SendApproveCommand_WorkflowApproved(CancellationToken ct)
    {
        // Arrange
        var wfObj = new TaskWorkflowObject();

        // Act
        var wi = await this.StartWorkflow(wfObj, ct);

        var preWfObjStatus = wfObj.Status;

        var approveEvent = (await this.Storage.GetWaitEvents(ct)).Single(ei => ei.Event == TaskWorkflow.ApproveEventHeader);
        await this.Host.PushEvent(approveEvent.Event, approveEvent.TargetState);

        // Assert
        preWfObjStatus.Should().Be(TaskApproveStatus.Approving);
        wfObj.Status.Should().Be(TaskApproveStatus.Approved);

        wfObj.PostProcessWork.Should().BeTrue();

        wi.Status.Should().Be(WorkflowStatus.Finished);

        (await this.Storage.GetWaitEvents()).Should().BeEmpty();
    }
}