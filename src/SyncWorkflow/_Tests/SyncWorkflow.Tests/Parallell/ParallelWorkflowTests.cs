using FluentAssertions;

using SyncWorkflow.Engine;

namespace SyncWorkflow.Tests.Parallel;

public class ParallelWorkflowTests : SingleScopeWorkflowTestBase<ParallelWorkflowObject, ParallelWorkflow>
{
    [Fact]
    public async Task Parallel_SendApprove_RejectBranchTerminated()
    {
        // Arrange
        var wfObj = new ParallelWorkflowObject();

        // Act
        var wi = await this.StartWorkflow(wfObj);

        var preWfObjStatus = wfObj.Status;

        var waitEvents = (await this.Storage.GetWaitEvents());

        var approveEvent = waitEvents.Single(ei => ei.Event == ParallelWorkflow.ApproveWaitEvent);

        var rejectEvent = waitEvents.Single(ei => ei.Event == ParallelWorkflow.RejectWaitEvent);

        await this.Host.PushEvent(approveEvent.Event, approveEvent.TargetState);

        // Assert
        preWfObjStatus.Should().Be(ParallelApproveStatus.Approving);
        wfObj.Status.Should().Be(ParallelApproveStatus.Approved);

        approveEvent.TargetState.Workflow.Status.Should().Be(WorkflowStatus.Finished);
        rejectEvent.TargetState.Workflow.Status.Should().Be(WorkflowStatus.Terminated);

        wi.Status.Should().Be(WorkflowStatus.Finished);

        (await this.Storage.GetWaitEvents()).Should().BeEmpty();
    }
}