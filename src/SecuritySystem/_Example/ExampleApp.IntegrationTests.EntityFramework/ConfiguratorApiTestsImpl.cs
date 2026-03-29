using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class ConfiguratorApiTestsImpl() : ConfiguratorApiTests(TestEnvironmentImpl.Instance.RootServiceProvider);