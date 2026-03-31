using CommonFramework.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

namespace OData.DependencyInjection;

public class ODataBuilder : IODataBuilder, IServiceInitializer
{
    private Type cacheType = typeof(ODataCache<,>);

    public void Initialize(IServiceCollection services)
    {
        services.AddSingleton(typeof(IODataCache<,>), this.cacheType)
            .AddSingleton<ILambdaExpressionConverter, LambdaExpressionConverter>()
            .AddSingleton<IRawSelectOperationParser, RawSelectOperationParser>()
            .AddSingleton<ISelectOperationParser, SelectOperationParser>();
    }

    public IODataBuilder SetCacheType(Type newCacheType)
    {
        this.cacheType = newCacheType;

        return this;
    }
}