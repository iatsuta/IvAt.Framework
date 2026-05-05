using Anch.Workflow.Domain.Definition;

namespace Anch.Workflow.Builder.Default.DomainDefinition;

public interface IWorkflowDefinitionBuilder : IWorkflowDefinition
{
    void ReplaceAutoNames((IStateDefinitionBuilder State, int? Index)? ownerInfo = null);
}

//public interface IWorkflowDefinitionBuilder<TSource> : IWorkflowDefinitionBuilder, IWorkflowDefinition<TSource>
//    where TSource : notnull
//{
//    new PropertyAccessors<TSource, long>? VersionAccessors { get; set; }


//    PropertyAccessors<TSource, long>? IWorkflowDefinition<TSource>.VersionAccessors => this.VersionAccessors;

//}

//public interface IWorkflowDefinitionBuilder : IWorkflowDefinition
//{
//    new Dictionary<string, object> Settings { get; set; }

//    IReadOnlyDictionary<string, object> IWorkflowDefinition.Settings => this.Settings;
//}