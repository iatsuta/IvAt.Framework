namespace OData.DependencyInjection;

public interface IODataBuilder
{
    IODataBuilder SetCacheType(Type cacheType);
}