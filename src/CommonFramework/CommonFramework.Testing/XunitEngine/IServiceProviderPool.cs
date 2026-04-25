namespace CommonFramework.Testing.XunitEngine;

public interface IServiceProviderPool
{
    IServiceProvider Get();

    void Release(IServiceProvider serviceProvider);
}