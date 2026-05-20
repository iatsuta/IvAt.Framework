namespace Anch.Testing.Database.ConnectionStringManagement;

public class TestConnectionStringPostfixFactory : ITestConnectionStringPostfixFactory
{
    public string Create(TestConnectionStringRole testConnectionStringRole)
    {
        if (testConnectionStringRole == TestConnectionStringRole.EmptySnapshot)
        {
            return "empty";
        }
        else if (testConnectionStringRole == TestConnectionStringRole.FilledSnapshot)
        {
            return "filled";
        }
        else if (testConnectionStringRole is PoolTestConnectionStringRole poolTestConnectionStringRole)
        {
            if (poolTestConnectionStringRole.ServiceProviderIndex == ServiceProviderIndex.Main)
            {
                return "";
            }
            else
            {
                return $"{poolTestConnectionStringRole.ServiceProviderIndex.Index:D5}";
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(testConnectionStringRole), testConnectionStringRole, null);
        }
    }
}