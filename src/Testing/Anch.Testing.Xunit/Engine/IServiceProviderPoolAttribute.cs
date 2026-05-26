namespace Anch.Testing.Xunit.Engine;

public interface IServiceProviderPoolAttribute
{
    IServiceProviderPool? ServiceProviderPool { get; set; }
}