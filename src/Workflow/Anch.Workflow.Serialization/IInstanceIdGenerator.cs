namespace Anch.Workflow.Serialization;

public interface IInstanceIdGenerator<in TElement>
{
    Guid GenerateId(TElement element);
}