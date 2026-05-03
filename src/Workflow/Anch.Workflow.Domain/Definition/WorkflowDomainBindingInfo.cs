using Anch.Core;

namespace Anch.Workflow.Domain.Definition;

public record WorkflowDomainBindingInfo<TSource, TStatus> : WorkflowDomainBindingInfo<TSource>
{
    public required PropertyAccessors<TSource, TStatus> Status { get; init; }

    public sealed override Type? StatusType { get; } = typeof(TStatus);
}

public record WorkflowDomainBindingInfo<TSource> : WorkflowDomainBindingInfo
{
    public sealed override Type SourceType { get; } = typeof(TSource);

    public PropertyAccessors<TSource, long>? Version { get; init; }
}

public abstract record WorkflowDomainBindingInfo
{
    public abstract Type SourceType { get; }

    public virtual Type? StatusType { get; } = null;
}