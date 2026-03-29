using GenericQueryable.IntegrationTests.Environment;

namespace GenericQueryable.IntegrationTests;

public class MainTestsImpl() : MainTests(TestEnvironmentImpl.Instance)
{
    [Fact]
    public override Task DefaultGenericQueryable_InvokeToListAsync_MethodInvoked()
    {
        return base.DefaultGenericQueryable_InvokeToListAsync_MethodInvoked();
    }
}