using System.Collections.Concurrent;
using System.Reflection;

using Xunit.v3;

namespace CommonFramework.Testing.XunitEngine;

public class CommonTestFramework : XunitTestFramework
{
    private readonly ConcurrentDictionary<Assembly, ITestEnvironment?> testEnvironmentCache = [];

    private readonly ConcurrentDictionary<Assembly, IServiceProviderPool?> rootServiceProviderPoolCache = [];

    private ITestEnvironment? GetTestEnvironment(Assembly assembly)
    {
        return this.testEnvironmentCache.GetOrAdd(assembly, asm =>
        {
            var commonTestFrameworkAttribute = asm.GetCustomAttribute<CommonTestFrameworkAttribute>()
                                               ?? throw new InvalidOperationException(
                                                   $"Assembly '{asm.FullName}' must be decorated with '{typeof(CommonTestFrameworkAttribute).FullName}' attribute");

            return commonTestFrameworkAttribute.TestEnvironmentType == null
                ? null
                : Activator.CreateInstance(commonTestFrameworkAttribute.TestEnvironmentType) as ITestEnvironment
                  ?? throw new InvalidOperationException(
                      $"Failed to create initializer of type '{commonTestFrameworkAttribute.TestEnvironmentType.FullName}'");
        });
    }

    private IServiceProviderPool? GetServiceProviderPool(Assembly assembly) =>

        this.rootServiceProviderPoolCache.GetOrAdd(assembly,
            _ => this.GetTestEnvironment(assembly) is { } testEnvironment
                ? new ServiceProviderPool(testEnvironment)
                : null);

    protected override ITestFrameworkExecutor CreateExecutor(Assembly assembly)
    {
        var rootRunner =
            new CommonTestAssemblyRunner(
                new CommonTestCollectionRunner(new CommonTestClassRunner(this.GetServiceProviderPool(assembly))));

        return new CommonFrameworkExecutor(new XunitTestAssembly(assembly), rootRunner);
    }

    protected override ITestFrameworkDiscoverer CreateDiscoverer(Assembly assembly) =>

        new CommonFrameworkDiscoverer(new XunitTestAssembly(assembly, null, assembly.GetName().Version),
            this.GetServiceProviderPool(assembly));
}