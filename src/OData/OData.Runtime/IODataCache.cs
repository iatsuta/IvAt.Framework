namespace OData;

/// <summary>
/// Formal abstraction over a cache, allowing it to be implemented using IMemoryCache
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IODataCache<TKey, TValue>
{
    TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);
}