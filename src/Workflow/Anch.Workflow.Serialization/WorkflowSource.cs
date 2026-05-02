using Anch.Core;
using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Serialization;

public class WorkflowSource : IWorkflowSource
{
    private readonly Lazy<Dictionary<WorkflowDefinitionIdentity, IWorkflowDefinition>> lazyStorageDict;

    public WorkflowSource(IEnumerable<IWorkflowDefinition> rootWorkflows)
    {
        this.lazyStorageDict = LazyHelper.Create(() =>
        {
            return rootWorkflows
                .GetAllElements(wfRef => wfRef.States.SelectMany(state => state.SubWorkflow))
                .ToDictionary(wfRef => wfRef.Identity);
        });
    }

    public IReadOnlyDictionary<WorkflowDefinitionIdentity, IWorkflowDefinition> GetWorkflows()
    {
        return this.lazyStorageDict.Value;
    }
}