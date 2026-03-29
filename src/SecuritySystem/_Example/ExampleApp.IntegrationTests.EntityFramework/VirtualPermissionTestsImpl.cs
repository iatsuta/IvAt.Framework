using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class VirtualPermissionTestsImpl() : VirtualPermissionTests(TestEnvironmentImpl.Instance.RootServiceProvider);