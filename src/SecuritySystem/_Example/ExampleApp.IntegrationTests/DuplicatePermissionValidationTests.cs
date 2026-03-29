using ExampleApp.Application;
using ExampleApp.Domain;

using SecuritySystem;
using SecuritySystem.Validation;

namespace ExampleApp.IntegrationTests;

public abstract class DuplicatePermissionValidationTests(IServiceProvider rootServiceProvider) : TestBase(rootServiceProvider)
{
    [Fact]
    public async Task AddRoleAsync_WhenDuplicatePermissionExists_ShouldThrowValidationException()
    {
        // Arrange
        var principalName = "TestPrincipal";
        var buIdentity = await this.AuthManager.GetSecurityContextIdentityAsync<BusinessUnit, Guid>("TestRootBu", this.CancellationToken);

        Task<SecurityIdentity> Assign() => this.AuthManager.For(principalName)
            .AddRoleAsync(new TestPermission(ExampleSecurityRole.BuManager) { BusinessUnit = buIdentity }, this.CancellationToken);

        await Assign();

        // Act
        var action = Assign;

        // Assert
        var error = await action.Should().ThrowAsync<SecuritySystemValidationException>();

        error.And.Message.Should().Contain($"Principal \"{principalName}\" has duplicate permissions");
    }


    [Fact]
    public async Task AddRoleAsync_WhenPermissionPeriodsDoNotIntersect_ShouldNotThrow()
    {
        // Arrange
        var principalName = "TestPrincipal";
        var buIdentity = await this.AuthManager.GetSecurityContextIdentityAsync<BusinessUnit, Guid>("TestRootBu", this.CancellationToken);

        Task<SecurityIdentity> Assign(PermissionPeriod period) => this.AuthManager.For(principalName)
            .AddRoleAsync(new TestPermission(ExampleSecurityRole.BuManager) { BusinessUnit = buIdentity, Period = period }, this.CancellationToken);

        await Assign(new PermissionPeriod(new DateTime(2000, 1, 1), new DateTime(2009, 1, 1)));

        // Act
        var action = () => Assign(new PermissionPeriod(new DateTime(2010, 1, 1), new DateTime(2019, 1, 1)));

        // Assert
        await action.Should().NotThrowAsync();
    }
}