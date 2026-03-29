using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class NotificationTestsImpl() : NotificationTests(TestEnvironmentImpl.Instance.RootServiceProvider);