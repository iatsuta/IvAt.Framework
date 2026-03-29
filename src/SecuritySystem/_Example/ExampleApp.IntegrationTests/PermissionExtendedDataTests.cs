using ExampleApp.Application;

namespace ExampleApp.IntegrationTests;

public abstract class PermissionExtendedDataTests(IServiceProvider rootServiceProvider) : TestBase(rootServiceProvider)
{
    [Fact]
    public async Task SetRoleAsync_WithExtendedValue_ShouldPersistExtendedData()
    {
        // Arrange
        var principalName = "TestPrincipal";

        var extendedValue = "abc";

        var testPermission = new TestPermission(ExampleSecurityRole.DefaultRole) { ExtendedValue = extendedValue };

        // Act
        var principalIdentity = await this.AuthManager.For(principalName).SetRoleAsync(testPermission, this.CancellationToken);

        // Assert
        var managedPrincipal = await this.AuthManager.For(principalIdentity).GetPrincipalAsync(this.CancellationToken);

        var managedPermission = managedPrincipal.Permissions.Should().ContainSingle().Subject;

        managedPermission.ExtendedData.GetValueOrDefault(TestPermissionExtensions.ExtendedKey)
            .Should().Be(extendedValue);
    }
}