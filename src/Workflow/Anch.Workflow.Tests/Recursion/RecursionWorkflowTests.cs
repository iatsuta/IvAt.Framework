using Anch.Testing.Xunit;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Tests._Base;

namespace Anch.Workflow.Tests.Recursion;

public class RecursionWorkflowTests : SingleScopeWorkflowTestBase<RecursionWorkflowObject, RecursionWorkflow>
{
    [Theory]
    [AnchInlineData(4, 1000, 1006)]
    [AnchInlineData(11, 1000, 1055)]
    public async Task SumNumbersByRecurse_ResultEquals(int limit, int extraAddToResult, int result, CancellationToken ct)
    {
        // Arrange
        var wfObj = new RecursionWorkflowObject { Limit = limit, ExtraAddToResult = extraAddToResult };

        // Act
        var wi = await this.StartWorkflow(wfObj, ct);

        // Assert
        Assert.Equal(WorkflowStatus.Finished, wi.Status);
        Assert.Equal(result, wfObj.Result);

        Assert.Empty(await this.Storage.GetWaitEvents(ct));
    }
}