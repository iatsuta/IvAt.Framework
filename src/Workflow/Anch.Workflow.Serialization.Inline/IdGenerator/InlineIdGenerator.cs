using Anch.IdentitySource;
using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.Serialization.Inline.IdGenerator;

public class InlineIdGenerator<TElement>(Func<TElement, WorkflowInstance> pathToWi, IIdentityInfo<TElement, Guid> identityInfo) : IInstanceIdGenerator<TElement>
{
    public Guid GenerateId(TElement element)
    {
        throw new NotImplementedException();
        //var wi = pathToWi(element);

        //return (Guid)getIdDelegate.DynamicInvoke(wi.Source)!;
    }
}