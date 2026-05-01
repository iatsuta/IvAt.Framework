using Anch.Workflow.Engine;

namespace Anch.Workflow.States;

public class ParallelForeachState<TSource, TElement>(IWorkflowMachineFactory workflowMachineFactory) : ParallelStateBase<TSource>
{
    private List<TElement> elements = [];


    public IEnumerable<TElement> Elements
    {
        get => this.elements;
        set => this.elements = value.ToList();
    }


    public IWorkflow<(TSource, TElement)> ElementWorkflow { get; set; } = null!;

    protected override IEnumerable<IWorkflowMachine> CreateChildMachines(TSource source)
    {
        return this.Elements.Select(childrenElement => workflowMachineFactory.Create((source, childrenElement), this.ElementWorkflow));
    }
}