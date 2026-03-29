using ExampleApp.IntegrationTests.Environment;

namespace ExampleApp.IntegrationTests;

public class ImpersonateServiceTestsImpl() : ImpersonateServiceTests(TestEnvironmentImpl.Instance.RootServiceProvider);