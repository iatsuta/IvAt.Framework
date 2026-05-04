using Anch.Testing.Xunit;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Tests._Base;

namespace Anch.Workflow.Tests.Output;

public class BindingWorkflowTests : SingleScopeWorkflowTestBase<BindingWorkflowObject, BindingWorkflow>
{
    [Theory]
    [AnchInlineData(2, 3, 5)]
    [AnchInlineData(6, 7, 13)]
    public async ValueTask BindingInputAndOutputPropsByState_ResultEquals(int v1, int v2, int expectedResult, CancellationToken ct)
    {
        // Arrange
        var wfObj = new BindingWorkflowObject { Value1 = v1, Value2 = v2 };

        // Act
        var wi = await this.StartWorkflow(wfObj, ct);

        // Assert
        Assert.Equal(WorkflowStatus.Finished, wi.Status);
        Assert.Equal(expectedResult, wfObj.Result);

        Assert.Empty(await this.RootRepository.GetWaitEvents().ToListAsync(ct));
    }
}