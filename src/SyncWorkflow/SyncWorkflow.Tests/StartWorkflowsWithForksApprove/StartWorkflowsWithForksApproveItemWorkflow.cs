using SyncWorkflow.Builder;
using SyncWorkflow.Builder.Default;
using SyncWorkflow.States;

namespace SyncWorkflow.Tests.StartWorkflowsWithForksApprove;

public class StartWorkflowsWithForksApproveItemWorkflow : BuildWorkflow<StartWorkflowsWithForksApproveItemWorkflowObject>
{
    public static readonly EventHeader ApproveWaitEvent = new(nameof(ApproveWaitEvent));

    public static readonly EventHeader RejectWaitEvent = new(nameof(RejectWaitEvent));

    protected override void Build(IWorkflowBuilder<StartWorkflowsWithForksApproveItemWorkflowObject> builder)
    {
        builder
            .Then(wfObj => wfObj.Status = StartWorkflowsWithForksApproveStatus.Approving)
            .Parallel(

                approveBranch => approveBranch
                    .Then<WaitEventState>()
                    .Input(s => s.Event, ApproveWaitEvent)
                    .Output(wfObj => wfObj.Status, StartWorkflowsWithForksApproveStatus.Approved),


                rejectBranch => rejectBranch
                    .Then<WaitEventState>()
                    .Input(s => s.Event, RejectWaitEvent)
                    .Output(wfObj => wfObj.Status, StartWorkflowsWithForksApproveStatus.Rejected))

            .SetBreak(StateBreakPolicy.WaitAny);
    }
}