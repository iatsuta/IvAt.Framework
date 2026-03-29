using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class GeneralPermissionTestsImpl() : GeneralPermissionTests(TestEnvironmentImpl.Instance.RootServiceProvider);