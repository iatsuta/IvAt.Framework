using Xunit.Internal;
using Xunit.Sdk;
using Xunit.v3;

namespace CommonFramework.Testing.XunitEngine;

public class CommonTestCollectionRunner(CommonTestClassRunner commonTestClassRunner, IServiceProviderPool? serviceProviderPool) :
    XunitTestCollectionRunnerBase<CommonTestCollectionRunnerContext, IXunitTestCollection, IXunitTestClass, IXunitTestCase>
{
    public async ValueTask<RunSummary> Run(
        IXunitTestClass testClass,
        IReadOnlyCollection<IXunitTestCase> testCases,
        ExplicitOption explicitOption,
        IMessageBus messageBus,
        ITestCaseOrderer testCaseOrderer,
        ExceptionAggregator aggregator,
        CancellationTokenSource cancellationTokenSource,
        FixtureMappingManager collectionFixtureMappings)
    {
        Guard.ArgumentNotNull(testClass);
        Guard.ArgumentNotNull(testCases);
        Guard.ArgumentNotNull(messageBus);
        Guard.ArgumentNotNull(testCaseOrderer);
        Guard.ArgumentNotNull(cancellationTokenSource);
        Guard.ArgumentNotNull(collectionFixtureMappings);

        var serviceProvider = serviceProviderPool?.Get();

        try
        {
            await using var ctxt = new CommonTestClassRunnerContext(
                testClass,
                @testCases,
                explicitOption,
                messageBus,
                testCaseOrderer,
                aggregator,
                cancellationTokenSource,
                collectionFixtureMappings,
                serviceProvider);

            await ctxt.InitializeAsync();

            return await ctxt.Aggregator.RunAsync(() => this.Run(ctxt), default);
        }
        finally
        {
            if (serviceProviderPool != null && serviceProvider is not null)
            {
                serviceProviderPool.Release(serviceProvider);
            }
        }
    }

    protected override ValueTask<RunSummary> RunTestClass(CommonTestCollectionRunnerContext ctxt, IXunitTestClass? testClass, IReadOnlyCollection<IXunitTestCase> testCases)
    {
        throw new NotImplementedException();
    }


    protected override ValueTask<RunSummary> RunTestMethod2(CommonTestCollectionRunnerContext ctxt, IXunitTestMethod? testMethod, IReadOnlyCollection<IXunitTestCase> testCases, object?[] constructorArguments)
    {
        Guard.ArgumentNotNull(ctxt);

        // Technically not possible because of the design of TTestClass, but this signature is imposed
        // by the base class, which allows method-less tests
        if (testMethod is null)
            return new(XunitRunnerHelper.FailTestCases(
                ctxt.MessageBus,
                ctxt.CancellationTokenSource,
                testCases,
                "Test case '{0}' does not have an associated method and cannot be run by XunitTestMethodRunner",
                sendTestMethodMessages: true
            ));

        return commonTestClassRunner.Run(
            testMethod,
            testCases,
            ctxt.ExplicitOption,
            ctxt.MessageBus,
            ctxt.Aggregator.Clone(),
            ctxt.CancellationTokenSource,
            constructorArguments,
            ctxt.ServiceProvider);
    }
}