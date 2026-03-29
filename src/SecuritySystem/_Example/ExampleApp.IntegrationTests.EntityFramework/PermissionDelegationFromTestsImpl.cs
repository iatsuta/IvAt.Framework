using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class PermissionDelegationFromTestsImpl() : PermissionDelegationFromTests(TestEnvironmentImpl.Instance.RootServiceProvider);