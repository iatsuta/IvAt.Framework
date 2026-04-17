using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using SyncWorkflow.Engine;

namespace SyncWorkflow.Tests.Condition;

public class ConditionWorkflowTests : SingleScopeWorkflowTestBase<ConditionWorkflowObject, ConditionWorkflow>
{
    [Theory]
    [InlineData(100, true)]
    [InlineData(101, false)]
    public async Task UseIf_CodeSwitched_Cases(int value, bool isEven)
    {
        // Arrange
        var wfObj = new ConditionWorkflowObject { Value = value };

        var expectedResult = ConditionWorkflow.BuildResult(wfObj.Value, isEven);

        // Act
        var wi = await this.StartWorkflow(wfObj);

        // Assert
        wi.Status.Should().Be(WorkflowStatus.Finished);
        wfObj.Result.Should().Be(expectedResult);

        (await this.Storage.GetWaitEvents()).Should().BeEmpty();
    }

    protected override IServiceCollection CreateServices()
    {
        return base.CreateServices()
            .AddSingleton<ConditionWorkflowService>();
    }
}