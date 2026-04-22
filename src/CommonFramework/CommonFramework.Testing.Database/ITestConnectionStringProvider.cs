namespace CommonFramework.Testing.Database;

public interface ITestConnectionStringProvider
{
    string CreateWithPostfix(string postfix);
}