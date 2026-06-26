using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SmartAccCloud.Application.Caching;

public interface IRedisCacheService
{
    Task RemoveCacheAsync(IDistributedCache cache, string key, CancellationToken token = default);
    Task RemoveByPatternAsync(string pattern);
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null) where T : class;
    Task RemoveCacheAsync(string key, CancellationToken token);
}