using CommonFramework.DependencyInjection;
using CommonFramework.Testing.DiTests;

using Microsoft.Extensions.DependencyInjection;

[assembly:CommonTestFramework<TestEnvironment>]

namespace CommonFramework.Testing.DiTests;

public class TestEnvironment : ITestEnvironment
{
    public IServiceProvider BuildServiceProvider(IServiceCollection services)
    {
        return new ServiceCollection()
            .AddValidator<DuplicateServiceUsageValidator>()
            .Validate()
            .BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true });
    }
}