using Anch.Core;
using Anch.Workflow.Domain;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Execution;
using Anch.Workflow.Serialization;
using Anch.Workflow.StateFactory;
using Anch.Workflow.States;

namespace Anch.Workflow.Engine;

public class WorkflowStateMachine<TState, TSource>(
    IServiceProvider serviceProvider,
    IWorkflowHost host,
    IStateFactoryCache stateFactoryCache,
    ISpecificWorkflowStorage storage,
    StateInstance stateInstance)
    where TState : IState
    where TSource : notnull
{
    private WorkflowInstance WorkflowInstance => stateInstance.Workflow;

    private TSource Source => field ??= (TSource)this.WorkflowInstance.Source;

    private IStateFactory StateFactory => field ??= stateFactoryCache.GetStateFactory(stateInstance.Definition);

    private TState CodeState => field ??= (TState)this.StateFactory.CreateState(serviceProvider, this.Source);

    public async ValueTask Save(CancellationToken cancellationToken) =>

        await storage.SaveWorkflowInstance(this.WorkflowInstance, cancellationToken);



    protected virtual IExecutionContext CreateExecutionContext(CancellationToken cancellationToken, WaitEventInfo? callbackEventInfo = null) =>

        new ExecutionContext
        {
            StateInstance = stateInstance,
            CancellationToken = cancellationToken,
            CallbackEventInfo = callbackEventInfo
        };

    public async ValueTask<WorkflowProcessResult> ProcessCurrentState(CancellationToken cancellationToken)
    {
        if (!stateInstance.IsActual)
        {
            return WorkflowProcessResult.Empty;
        }
    }

        await this.ProcessCurrentState(this.CreateExecutionContext(cancellationToken));

    public virtual async ValueTask ProcessInput(CancellationToken cancellationToken)
    {
        this.WorkflowInstance.SetStatus(WorkflowStatus.Runnable);

        if (!stateInstance.InputProcessed)
        {
            stateInstance.InputProcessed = true;

            await this.StateFactory.BindInput(this.CodeState, serviceProvider, this.Source, cancellationToken);
        }

        await this.Save(cancellationToken);
    }

    public virtual async ValueTask<WorkflowProcessResult> Run(CancellationToken cancellationToken)
    {
        var executionResult = await this.CodeState.Run(this.CreateExecutionContext(cancellationToken));

        var mainResult = new WorkflowProcessResult([this.WorkflowInstance],
            [new UnprocessedStateResult(stateInstance, new ExecutionResultUnprocessedStateData(executionResult))]);

        var leaveResult = executionResult.LeaveState ? new WorkflowProcessResult([],)

        return ;
    }

    public virtual async ValueTask<WorkflowProcessResult> ProcessOutput(CancellationToken cancellationToken)
    {
        if (!stateInstance.OutputProcessed)
        {
            stateInstance.OutputProcessed = true;

            await this.StateFactory.BindOutput(this.CodeState, serviceProvider, executionContext.Source, executionContext.CancellationToken);

            var leaveResult = await this.CodeState.LeavePolicy.Leave(serviceProvider, executionContext);

            return workflowProcessResult + leaveResult;
        }
    }
    public virtual async ValueTask<WorkflowProcessResult> ProcessCurrentState(IExecutionContext executionContext)
    {


 <---- WARN

        if (!stateInstance.OutputProcessed && executionResult.LeaveState)
        {
            stateInstance.OutputProcessed = true;

            await this.StateFactory.BindOutput(this.CodeState, serviceProvider, executionContext.Source, executionContext.CancellationToken);

            var leaveResult = await this.CodeState.LeavePolicy.Leave(serviceProvider, executionContext);

            return workflowProcessResult + leaveResult;
        }
        else
        {
            return workflowProcessResult;
        }
    }

    public virtual async ValueTask<WorkflowProcessResult> ProcessExecutionResult(IExecutionResult executionResult, CancellationToken cancellationToken)
    {
        switch (executionResult)
        {
            case WorkflowProcessExecutionResult workflowProcessExecutionResult:
            {
                if (workflowProcessExecutionResult.LeaveState)
                {
                    return workflowProcessExecutionResult.WorkflowProcessResult +

                           await this.ProcessExecutionResult(stateInstance, new PushEventResult(EventHeader.StateDone, stateInstance), cancellationToken);
                }
                else
                {
                    return workflowProcessExecutionResult.WorkflowProcessResult;
                }
            }

            case Wait:
                this.WorkflowInstance.SetStatus(WorkflowStatus.WaitEvent);
                return WorkflowProcessResult.Empty;

            case WaitEventResult waitEventResult:
                this.WorkflowInstance.SetStatus(WorkflowStatus.WaitEvent);
                stateInstance.RegisterWaitEvent(waitEventResult.ToEventInfo(stateInstance));
                return WorkflowProcessResult.Empty;

            case Done:
                return await this.ProcessExecutionResult(stateInstance, new PushEventResult(EventHeader.StateDone, stateInstance), cancellationToken);

            case PushEventResult pushEventResult:
                return await this.ProcessExecutionResult(stateInstance, pushEventResult, cancellationToken);

            case MultiExecutionResult multiExecutionResult:
                return new WorkflowProcessResult([],
                    [.. multiExecutionResult.ExecutionResults.Select(er => new UnprocessedStateResult(stateInstance, er))]);

            default:
                throw new ArgumentOutOfRangeException(nameof(executionResult));
        }
    }

    private async ValueTask<WorkflowProcessResult> ProcessExecutionResult(StateInstance stateInstance, PushEventResult pushEventResult,
        CancellationToken cancellationToken)
    {
        return await this.ProcessExecutionResult(stateInstance, pushEventResult.ToEventInfo(this.WorkflowInstance), cancellationToken);
    }

    private async ValueTask<WorkflowProcessResult> ProcessExecutionResult(StateInstance stateInstance, PushEventInfo pushEventInfo,
        CancellationToken cancellationToken)
    {
        if (pushEventInfo.TargetState == null && pushEventInfo.Header.IsGlobal)
        {
            if (pushEventInfo.Header == EventHeader.WorkflowFinished)
            {
                if (this.WorkflowInstance.Status.Role != WorkflowStatusRole.Finished)
                {
                    this.WorkflowInstance.SetStatus(WorkflowStatus.Finished);
                }
            }
            else if (pushEventInfo.Header == EventHeader.WorkflowTerminated)
            {
                this.WorkflowInstance.SetStatus(WorkflowStatus.Terminated);
            }

            return await host.CreateExecutor(WorkflowExecutionPolicy.SingleStep).PushEvent(pushEventInfo, cancellationToken);
        }
        else if (pushEventInfo.TargetState.Maybe(s => s.Workflow != this.WorkflowInstance))
        {
            return await host.CreateExecutor(WorkflowExecutionPolicy.SingleStep).PushEvent(pushEventInfo, cancellationToken);
        }
        else
        {
            var transition = stateInstance.Definition.Transitions.Single(tr => tr.Event.Header == pushEventInfo.Header);

            return await this.SwitchState(transition.To, cancellationToken);
        }
    }

    public async ValueTask<WorkflowProcessResult> PushReleasedEvent(WaitEventInfo releasedEventInfo, CancellationToken cancellationToken)
    {
        var executionContext = this.CreateExecutionContext(releasedEventInfo.TargetState, cancellationToken, releasedEventInfo);

        return await this.ProcessCurrentState(executionContext);
    }
}