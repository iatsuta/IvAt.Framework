using System.Collections.Concurrent;

namespace Anch.Testing.Database.ConnectionStringManagement;

public interface ITestConnectionStringPostfixFactory
{
    string Create(TestConnectionStringRole testConnectionStringRole);
}