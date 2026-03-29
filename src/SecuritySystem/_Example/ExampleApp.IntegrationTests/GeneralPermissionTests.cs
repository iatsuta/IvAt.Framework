using CommonFramework.GenericRepository;

using ExampleApp.Application;
using ExampleApp.Domain;

using GenericQueryable;

using Microsoft.Extensions.DependencyInjection;

using SecuritySystem;
using SecuritySystem.AvailableSecurity;
using SecuritySystem.DomainServices;

namespace ExampleApp.IntegrationTests;

public abstract class GeneralPermissionTests(IServiceProvider rootServiceProvider) : TestBase(rootServiceProvider)
{
    [Fact]
    public async Task SetRoleAsync_ShouldPreservePermissionIdentity()
    {
        // Arrange
        var testPermission = new TestPermission(ExampleSecurityRole.DefaultRole) { Identity = TypedSecurityIdentity.Create(Guid.NewGuid()) };

        // Act
        var principalIdentity = await this.AuthManager.For("TestPrincipal").SetRoleAsync(testPermission, this.CancellationToken);

        // Assert
        var managedPrincipal = await this.AuthManager.For(principalIdentity).GetPrincipalAsync(this.CancellationToken);

        var managedPermission = managedPrincipal.Permissions.Should().ContainSingle().Subject;

        managedPermission.Identity.Should().Be(testPermission.Identity);
    }

    [Fact]
    public async Task AssignGeneralPermission_PermissionResolved()
    {
        // Arrange
        var principalName = "TestPrincipal";

        var buIdentity = await this.AuthManager.GetSecurityContextIdentityAsync<BusinessUnit, Guid>("TestRootBu", this.CancellationToken);

        var testRole = ExampleSecurityRole.BuManager;

        var testPermission = new TestPermission(testRole) { BusinessUnit = buIdentity };

        var principalIdentity = await this.AuthManager.For(principalName).SetRoleAsync(testPermission, this.CancellationToken);
        this.AuthManager.For(principalIdentity).LoginAs();

        // Act
        var availableSecurityRoles = await this.GetEvaluator<IAvailableSecurityRoleSource>().EvaluateAsync(TestingScopeMode.Read,
            async availableSecurityRoleSource => await availableSecurityRoleSource.GetAvailableSecurityRoles().ToArrayAsync(this.CancellationToken));

        // Assert
        availableSecurityRoles.Should().BeEquivalentTo([testRole]);

        var managedPrincipal = await this.AuthManager.For(principalName).GetPrincipalAsync(this.CancellationToken);

        managedPrincipal.Header.Identity.Should().Be(principalIdentity);
        managedPrincipal.Header.Name.Should().Be(principalName);
        managedPrincipal.Header.IsVirtual.Should().Be(false);

        var managedPermission = managedPrincipal.Permissions.Should().ContainSingle().Subject;

        managedPermission.IsVirtual.Should().Be(false);
        managedPermission.Restrictions.Should().BeEquivalentTo(testPermission.Restrictions);
    }

    [Fact]
    public async Task AssignGeneralPermission_WithRootBu_AllTestObjectsResolved()
    {
        // Arrange
        var buIdentity = await this.AuthManager.GetSecurityContextIdentityAsync<BusinessUnit, Guid>("TestRootBu", this.CancellationToken);

        var testRole = ExampleSecurityRole.BuManager;

        var testPermission = new TestPermission(testRole) { BusinessUnit = buIdentity };

        var principalId = await this.AuthManager.For("TestPrincipal").SetRoleAsync([testPermission, ExampleSecurityRole.DefaultRole], this.CancellationToken);
        this.AuthManager.For(principalId).LoginAs();

        // Act

        // Assert
        await this.GetEvaluator<IServiceProvider>().EvaluateAsync(TestingScopeMode.Read, async sp =>
        {
            var testObjectDomainSecurityService = sp.GetRequiredService<IDomainSecurityService<TestObject>>();
            var securityProvider = testObjectDomainSecurityService.GetSecurityProvider(testRole);

            var queryableSource = sp.GetRequiredService<IQueryableSource>();

            var testObjectRepositoryFactory = sp.GetRequiredService<IRepositoryFactory<TestObject>>();
            var testObjectRepository = testObjectRepositoryFactory.Create(testRole);

            var expectedResult = await queryableSource.GetQueryable<TestObject>().GenericToListAsync(this.CancellationToken);

            var result = await testObjectRepository.GetQueryable().GenericToListAsync(this.CancellationToken);

            result.OrderBy(v => v.Id).Should().BeEquivalentTo(expectedResult.OrderBy(v => v.Id));

            foreach (var testObject in result)
            {
                (await securityProvider.HasAccessAsync(testObject, this.CancellationToken)).Should().Be(true);
            }
        });
    }
}