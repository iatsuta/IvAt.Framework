using Anch.Testing.Xunit;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Tests._Base;

namespace Anch.Workflow.Tests.Switch;

public class SwitchWorkflowTests : SingleScopeWorkflowTestBase<SwitchWorkflowObject, SwitchWorkflow>
{
    [Theory]
    [AnchInlineData(0, "0 DefaultCase")]
    [AnchInlineData(1, "1 Case 1")]
    [AnchInlineData(2, "2 Case 2")]
    [AnchInlineData(3, "3 Case 3")]
    [AnchInlineData(4, "4 DefaultCase")]
    public async Task UseIf_CodeSwitched_Cases(int value, string expectedResult, CancellationToken ct)
    {
        // Arrange
        var wfObj = new SwitchWorkflowObject { Value = value };

        // Act
        var wi = await this.StartWorkflow(wfObj, ct);

        // Assert
        Assert.Equal(WorkflowStatus.Finished, wi.Status);
        Assert.Equal(expectedResult, wfObj.Result);

        Assert.Empty(await this.Storage.GetWaitEvents(ct));
    }
}