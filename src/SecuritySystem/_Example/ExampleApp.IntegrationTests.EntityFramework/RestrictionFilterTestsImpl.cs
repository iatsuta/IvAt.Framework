using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class RestrictionFilterTestsImpl() : RestrictionFilterTests(TestEnvironmentImpl.Instance.RootServiceProvider);