using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class RunAsManagerTestsImpl() : RunAsManagerTests(TestEnvironmentImpl.Instance.RootServiceProvider);