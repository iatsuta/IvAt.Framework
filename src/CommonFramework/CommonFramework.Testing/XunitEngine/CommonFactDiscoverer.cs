using Xunit.Sdk;
using Xunit.v3;

namespace CommonFramework.Testing.XunitEngine;

public class CommonFactDiscoverer : FactDiscoverer
{
    public override ValueTask<IReadOnlyCollection<IXunitTestCase>> Discover(
        ITestFrameworkDiscoveryOptions discoveryOptions,
        IXunitTestMethod testMethod,
        IFactAttribute factAttribute)
    {
        if (testMethod.Method.LastParameterIsCt())
        {
            return new([this.CreateTestCase(discoveryOptions, testMethod, factAttribute)]);
        }
        else
        {
            return base.Discover(discoveryOptions, testMethod, factAttribute);
        }
    }
}