using CommonFramework.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using SecuritySystem.Services;

namespace SecuritySystem.Testing.DependencyInjection;

public class SecuritySystemTestingBuilder : ISecuritySystemTestingBuilder
{
    private Type evaluatorType = typeof(TestingEvaluator<>);

    private Func<IServiceProvider, TestRootUserInfo> getTestRootUserInfoFunc = _ => TestRootUserInfo.Default;

    public ISecuritySystemTestingBuilder SetEvaluator(Type newEvaluatorType)
    {
        this.evaluatorType = newEvaluatorType;

        return this;
    }

    public ISecuritySystemTestingBuilder SetTestRootUserInfo(Func<IServiceProvider, TestRootUserInfo> getInfo)
    {
        this.getTestRootUserInfoFunc = getInfo;

        return this;
    }

    public void Initialize(IServiceCollection services)
    {
        services

            .AddSingleton<RootImpersonateServiceState>()
            .AddSingleton<IRootImpersonateService, RootImpersonateService>()

            .AddScoped<TestingUserAuthenticationService>()
            .ReplaceScopedFrom<IRawUserAuthenticationService, TestingUserAuthenticationService>()
            .ReplaceScopedFrom<IImpersonateService, TestingUserAuthenticationService>()

            .AddScoped(typeof(UserCredentialManager))

            .AddSingleton<RootAuthManager>()
            .AddSingleton(AdministratorsRoleList.Default)
            .AddSingleton(this.getTestRootUserInfoFunc)
            .AddSingleton(typeof(ITestingEvaluator<>), this.evaluatorType);
    }
}