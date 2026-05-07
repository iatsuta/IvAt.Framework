using Anch.Workflow.Domain.Runtime;

namespace Anch.Workflow.StateFactory;

public interface ICodeStateProcessorFactory
{
    ICodeStateProcessor Create(StateInstance stateInstance);
}