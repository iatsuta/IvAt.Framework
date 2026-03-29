namespace SecuritySystem.Testing.DependencyInjection;

public interface ISecuritySystemTestingBuilder
{
    ISecuritySystemTestingBuilder SetEvaluator(Type evaluatorType);

    ISecuritySystemTestingBuilder SetTestRootUserInfo(Func<IServiceProvider, TestRootUserInfo> getInfo);
}