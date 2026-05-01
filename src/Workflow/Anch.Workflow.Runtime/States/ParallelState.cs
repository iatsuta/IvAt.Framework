using Anch.Workflow.Engine;

namespace Anch.Workflow.States;

public class ParallelState<TSource>(IWorkflowMachineFactory workflowMachineFactory) : ParallelStateBase<TSource>
    where TSource : notnull
{
    private List<IWorkflow<TSource>> forks = [];


    public IEnumerable<IWorkflow<TSource>> Forks
    {
        get => this.forks;
        set => this.forks = value.ToList();
    }

    protected override IEnumerable<IWorkflowMachine> CreateChildMachines(TSource source)
    {
        return this.Forks.Select(fork => workflowMachineFactory.Create(source, fork));
    }
}