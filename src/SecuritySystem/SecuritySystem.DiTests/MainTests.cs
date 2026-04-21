using CommonFramework.DependencyInjection;
using CommonFramework.GenericRepository;
using CommonFramework.Testing;

using Microsoft.Extensions.DependencyInjection;

using SecuritySystem.AccessDenied;
using SecuritySystem.DiTests.DomainObjects;
using SecuritySystem.DiTests.Environment;
using SecuritySystem.DiTests.Rules;
using SecuritySystem.DiTests.Services;
using SecuritySystem.DomainServices;
using SecuritySystem.Providers;

using System.Collections.Frozen;
using System.Collections.Immutable;

namespace SecuritySystem.DiTests;

public class MainTests
{
    private readonly TestData testData = new();

    private readonly IServiceProvider rootServiceProvider;

    public MainTests()
    {
        this.rootServiceProvider = new CustomTestServiceProviderBuilder(this.testData).Build(new ServiceCollection());
    }


    [CommonFact]
    public async Task TestEmployeesSecurity_EmployeeHasAccessCorrect(CancellationToken ct)
    {
        // Arrange
        await using var scope = this.rootServiceProvider.CreateAsyncScope();

        var employeeDomainSecurityService = scope.ServiceProvider.GetRequiredService<IDomainSecurityService<Employee>>();
        var counterService = scope.ServiceProvider.GetRequiredService<BusinessUnitAncestorLinkSourceExecuteCounter>();
        var securityProvider = employeeDomainSecurityService.GetSecurityProvider(SecurityRule.View);

        // Act
        var result1 = await securityProvider.HasAccessAsync(testData.employee1, ct);
        var result2 = await securityProvider.HasAccessAsync(testData.employee2, ct);
        var result3 = await securityProvider.HasAccessAsync(testData.employee3, ct);

        // Assert
        result1.Should().BeTrue();
        result2.Should().BeTrue();
        result3.Should().BeFalse();

        counterService.Count.Should().Be(1);
    }

    [CommonFact]
    public async Task CheckEmployeeWithoutSecurity_ExceptionRaised(CancellationToken ct)
    {
        // Arrange
        await using var scope = this.rootServiceProvider.CreateAsyncScope();

        var employeeDomainSecurityService = scope.ServiceProvider.GetRequiredService<IDomainSecurityService<Employee>>();
        var accessDeniedExceptionService = scope.ServiceProvider.GetRequiredService<IAccessDeniedExceptionService>();

        var securityProvider = employeeDomainSecurityService.GetSecurityProvider(SecurityRule.View);

        // Act
        var checkAccessAction = () => securityProvider.CheckAccessAsync(testData.employee3, accessDeniedExceptionService, ct);

        // Assert
        await checkAccessAction.Should().ThrowAsync<AccessDeniedException>();
    }

    private class BusinessUnitAncestorLinkSourceExecuteCounter
    {
        public int Count { get; set; }
    }

    private class CustomTestServiceProviderBuilder(TestData testData) : TestEnvironment
    {
        protected override IServiceCollection CreateServices(IServiceCollection serviceCollection) =>

            base.CreateServices(serviceCollection)

                .AddScoped<BusinessUnitAncestorLinkSourceExecuteCounter>()
                .ReplaceScopedFrom<IQueryableSource, IServiceProvider>(sp => new TestQueryableSource { BaseQueryableSource = this.BuildQueryableSource(sp) });


        protected override IEnumerable<TestPermission> GetPermissions()
        {
            yield return new TestPermission(
                ExampleSecurityRole.TestRole,
                new Dictionary<Type, ImmutableArray<Guid>> { { typeof(BusinessUnit), [testData.bu1.Id] } }.ToFrozenDictionary());
        }

        private IQueryableSource BuildQueryableSource(IServiceProvider serviceProvider)
        {
            var queryableSource = Substitute.For<IQueryableSource>();

            queryableSource.GetQueryable<BusinessUnitDirectAncestorLink>()
                .Returns(this.GetBusinessUnitAncestorLinkSource(serviceProvider).AsQueryable());

            queryableSource.GetQueryable<Employee>()
                .Returns(new[] { testData.employee1, testData.employee2, testData.employee3, testData.employee4 }.AsQueryable());

            return queryableSource;
        }

        private IEnumerable<BusinessUnitDirectAncestorLink> GetBusinessUnitAncestorLinkSource(IServiceProvider serviceProvider)
        {
            var counter = serviceProvider.GetRequiredService<BusinessUnitAncestorLinkSourceExecuteCounter>();
            counter.Count++;

            yield return new BusinessUnitDirectAncestorLink { Ancestor = testData.bu1, Child = testData.bu1 };
            yield return new BusinessUnitDirectAncestorLink { Ancestor = testData.bu2, Child = testData.bu2 };
            yield return new BusinessUnitDirectAncestorLink { Ancestor = testData.bu3, Child = testData.bu3 };

            yield return new BusinessUnitDirectAncestorLink { Ancestor = testData.bu1, Child = testData.bu2 };
        }
    }

    public class TestData
    {
        public readonly BusinessUnit bu1;

        public readonly BusinessUnit bu2;

        public readonly BusinessUnit bu3;

        public readonly Employee employee1;

        public readonly Employee employee2;

        public readonly Employee employee3;

        public readonly Employee employee4;

        public TestData()
        {
            this.bu1 = new() { Id = Guid.NewGuid() };
            this.bu2 = new BusinessUnit() { Id = Guid.NewGuid(), Parent = this.bu1 };
            this.bu3 = new BusinessUnit() { Id = Guid.NewGuid() };

            this.employee1 = new Employee() { Id = Guid.NewGuid(), BusinessUnit = this.bu1 };
            this.employee2 = new Employee() { Id = Guid.NewGuid(), BusinessUnit = this.bu2 };
            this.employee3 = new Employee() { Id = Guid.NewGuid(), BusinessUnit = this.bu3 };
            this.employee4 = new Employee() { Id = Guid.NewGuid() };
        }
    }
}