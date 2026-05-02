using Anch.Testing.Xunit;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Tests._Base;

using WorkflowStatus = Anch.Workflow.Engine.WorkflowStatus;

namespace Anch.Workflow.Tests.Wait;

public class WaitWorkflowTests : SingleScopeWorkflowTestBase<WaitWorkflowSource, WaitWorkflow>
{
    [AnchFact]
    public async Task SendWaitedEvent_TestPassed(CancellationToken ct)
    {
        // Arrange

        // Act
        var wi = await this.StartWorkflow(new WaitWorkflowSource(), ct);

        var preWiStatus = wi.Status;

        var pushResult = await this.Host.CreateExecutor(WorkflowExecutionPolicy.Full)
            .PushEvent(new EventHeader(WaitWorkflow.WaitEventName), wi.CurrentState, WaitWorkflow.WaitEventData, ct);

        var processedWorkflowInstances = pushResult.Started.Select(wm => wm.WorkflowInstance).ToArray();
        // Assert
        Assert.Equal(WorkflowStatus.WaitEvent, preWiStatus);
        Assert.Contains(wi, processedWorkflowInstances);
        Assert.Equal(WorkflowStatus.Finished, wi.Status);

        Assert.Empty(await this.Storage.GetWaitEvents(ct));
    }
}