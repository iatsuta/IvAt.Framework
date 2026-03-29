using ExampleApp.Domain;

using SecuritySystem;
using SecuritySystem.AvailableSecurity;

namespace ExampleApp.IntegrationTests;

public abstract class ClientSecurityRuleTests(IServiceProvider rootServiceProvider) : TestBase(rootServiceProvider)
{
    [Fact]
    public async Task GetAvailableSecurityRules_ReturnsExpectedClientSecurityRules()
    {
        // Arrange
        var expectedResult = new DomainSecurityRule.ClientSecurityRule[]
        {
            new(nameof(BusinessUnit) + SecurityRule.View),
            new(nameof(TestObject) + SecurityRule.View)
        };

        // Act
        var result = await this.GetEvaluator<IAvailableClientSecurityRuleSource>().EvaluateAsync(TestingScopeMode.Read,
            async availableClientSecurityRuleSource =>
                await availableClientSecurityRuleSource.GetAvailableSecurityRules().ToArrayAsync(this.CancellationToken));

        // Assert
        result.OrderBy(v => v.Name).Should().BeEquivalentTo(expectedResult);
    }
}