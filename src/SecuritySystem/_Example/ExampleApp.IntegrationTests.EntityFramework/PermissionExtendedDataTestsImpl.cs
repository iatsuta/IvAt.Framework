using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class PermissionExtendedDataTestsImpl() : PermissionExtendedDataTests(TestEnvironmentImpl.Instance.RootServiceProvider);