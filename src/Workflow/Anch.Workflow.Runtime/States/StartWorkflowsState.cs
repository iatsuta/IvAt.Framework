using Anch.Workflow.Engine;

namespace Anch.Workflow.States;

public class StartWorkflowsState<TSource, TElement>(IWorkflowMachineFactory workflowMachineFactory) : ParallelStateBase<TSource>
    where TElement : notnull
{
    private List<TElement> elements = [];


    public IEnumerable<TElement> Elements
    {
        get => this.elements;
        set => this.elements = value.ToList();
    }


    public IWorkflow<TElement> ElementWorkflow { get; set; } = null!;


    protected override IEnumerable<IWorkflowMachine> CreateChildMachines(TSource source)
    {
        return this.Elements.Select(childrenElement => workflowMachineFactory.Create(childrenElement, this.ElementWorkflow));
    }
}