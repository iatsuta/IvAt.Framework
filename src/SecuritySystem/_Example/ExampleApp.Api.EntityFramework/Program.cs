using ExampleApp.Infrastructure.DependencyInjection;

namespace ExampleApp.Api;

public static class Program
{
    public static Task Main(string[] args) =>
        GenericProgram.Main(args, builder => builder.Services.AddEntityFrameworkInfrastructure(builder.Configuration));
}