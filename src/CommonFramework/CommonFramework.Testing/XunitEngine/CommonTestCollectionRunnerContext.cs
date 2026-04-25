using Xunit.Sdk;
using Xunit.v3;

namespace CommonFramework.Testing.XunitEngine;

public class CommonTestCollectionRunnerContext(
    IXunitTestCollection testCollection,
    IReadOnlyCollection<IXunitTestCase> testCases,
    ExplicitOption explicitOption,
    IMessageBus messageBus,
    ITestCaseOrderer testCaseOrderer,
    ExceptionAggregator aggregator,
    CancellationTokenSource cancellationTokenSource,
    FixtureMappingManager assemblyFixtureMappings,
    IServiceProvider? serviceProvider)
    : XunitTestCollectionRunnerContext(testCollection, testCases, explicitOption, messageBus, testCaseOrderer, aggregator,
        cancellationTokenSource, assemblyFixtureMappings)
{
    public IServiceProvider? ServiceProvider { get; } = serviceProvider;
}