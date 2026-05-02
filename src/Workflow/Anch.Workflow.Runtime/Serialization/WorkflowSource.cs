using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

public class WorkflowSource(IEnumerable<IWorkflow> rootWorkflows) : IWorkflowSource
{
    private readonly Lazy<Dictionary<WorkflowDefinitionIdentity, IWorkflowDefinition>> lazyStorageDict =
        LazyHelper.Create(() => rootWorkflows.Select(wf => wf.Definition)
            .GetAllElements(wfRef => wfRef.States.SelectMany(state => state.SubWorkflow))
            .ToDictionary(wfRef => wfRef.Identity));

    public IReadOnlyDictionary<WorkflowDefinitionIdentity, IWorkflowDefinition> GetWorkflows()
    {
        return this.lazyStorageDict.Value;
    }
}