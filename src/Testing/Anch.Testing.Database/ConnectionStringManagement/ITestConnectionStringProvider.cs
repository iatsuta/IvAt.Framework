namespace Anch.Testing.Database.ConnectionStringManagement;

public interface ITestConnectionStringProvider
{
    TestConnectionString GetConnectionString(TestConnectionStringRole role);
}