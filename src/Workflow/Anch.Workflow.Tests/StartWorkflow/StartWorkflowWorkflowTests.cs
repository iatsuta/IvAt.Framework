using Anch.Testing.Xunit;
using Anch.Workflow.DependencyInjection;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Tests._Base;
using Anch.Workflow.Tests.Wait;

using Microsoft.Extensions.DependencyInjection;

using WorkflowStatus = Anch.Workflow.Engine.WorkflowStatus;

namespace Anch.Workflow.Tests.StartWorkflow;

public class StartWorkflowWorkflowTests : SingleScopeWorkflowTestBase<WaitWorkflowSource, RootWorkflow>
{
    [AnchFact]
    public async Task StartRootWf_SendPushEventToChild_WorkflowFinished(CancellationToken ct)
    {
        // Arrange

        // Act
        var rootWi = await this.StartWorkflow(new WaitWorkflowSource(), ct);

        var allWi = await this.Storage.GetWorkflowInstances(ct);

        var subWf = allWi.Except([rootWi]).Single();

        var preWiStatus = rootWi.Status;
        var preChildWfStatus = subWf.Status;

        var pushResult = await this.Host
            .CreateExecutor(WorkflowExecutionPolicy.Full)
            .PushEvent(new EventHeader(WaitWorkflow.WaitEventName), subWf.CurrentState, WaitWorkflow.WaitEventData, ct);

        var processedWorkflowInstances = pushResult.Started.Select(wm => wm.WorkflowInstance).ToArray();

        // Assert
        Assert.Equal(WorkflowStatus.WaitEvent, preWiStatus);
        Assert.Equal(WorkflowStatus.WaitEvent, preChildWfStatus);

        Assert.Single(processedWorkflowInstances);
        Assert.Contains(subWf, processedWorkflowInstances);

        Assert.Equal(WorkflowStatus.Finished, rootWi.Status);
        Assert.Equal(WorkflowStatus.Finished, subWf.Status);

        Assert.Empty(await this.Storage.GetWaitEvents(ct));
    }

    protected override IServiceCollection CreateServices()
    {
        return base.CreateServices()
            .RegisterSyncWorkflowType<WaitWorkflow>();
    }
}