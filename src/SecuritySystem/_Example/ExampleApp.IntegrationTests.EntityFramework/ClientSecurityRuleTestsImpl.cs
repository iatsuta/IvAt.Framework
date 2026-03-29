using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class ClientSecurityRuleTestsImpl() : ClientSecurityRuleTests(TestEnvironmentImpl.Instance.RootServiceProvider);