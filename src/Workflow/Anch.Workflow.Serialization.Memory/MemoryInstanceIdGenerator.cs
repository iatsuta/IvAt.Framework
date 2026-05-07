namespace Anch.Workflow.Serialization.Memory;

public class MemoryInstanceIdGenerator<TElement> : IInstanceIdGenerator<TElement>
{
    public Guid GenerateId(TElement _)
    {
        return Guid.NewGuid();
    }
}