using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class DomainSecurityRuleCredentialTestsImpl() : DomainSecurityRuleCredentialTests(TestEnvironmentImpl.Instance.RootServiceProvider);
