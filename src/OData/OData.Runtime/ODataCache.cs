using System.Collections.Concurrent;

namespace OData;

public class ODataCache<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IODataCache<TKey, TValue>
    where TKey : notnull;