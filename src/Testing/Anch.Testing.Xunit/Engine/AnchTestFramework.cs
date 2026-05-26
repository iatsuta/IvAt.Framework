using System.Collections.Concurrent;
using System.Reflection;

using Xunit;
using Xunit.v3;

namespace Anch.Testing.Xunit.Engine;

public class AnchTestFramework : XunitTestFramework
{
    private readonly ConcurrentDictionary<Assembly, IServiceProviderPool?> serviceProviderPoolCache = [];

    private IServiceProviderPool? GetServiceProviderPool(Assembly assembly) =>

        this.serviceProviderPoolCache.GetOrAdd(assembly,
            _ =>
            {
                var collectionBehaviorAttribute = assembly.GetCustomAttribute<CollectionBehaviorAttribute>();

                return assembly.TryCreateTestEnvironment() is { } testEnvironment
                    ? new ServiceProviderPool(testEnvironment, !collectionBehaviorAttribute?.DisableTestParallelization)
                    : null;
            });

    protected override ITestFrameworkExecutor CreateExecutor(Assembly assembly)
    {
        var serviceProviderPool = this.GetServiceProviderPool(assembly);

        var rootRunner =
            new AnchTestAssemblyRunner(
                new AnchTestCollectionRunner(
                    new AnchTestClassRunner(), serviceProviderPool));

        return new AnchFrameworkExecutor(new XunitTestAssembly(assembly), rootRunner, serviceProviderPool);
    }

    protected override ITestFrameworkDiscoverer CreateDiscoverer(Assembly assembly) =>

        new AnchFrameworkDiscoverer(new XunitTestAssembly(assembly, null, assembly.GetName().Version),
            this.GetServiceProviderPool(assembly));
}