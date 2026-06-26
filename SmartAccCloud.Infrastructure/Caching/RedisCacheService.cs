using Newtonsoft.Json;
using SmartAccCloud.Application.Caching;
using StackExchange.Redis;

namespace SmartAccCloud.Infrastructure.Caching;

public class RedisCacheService(IConnectionMultiplexer redis): IRedisCacheService
{
    private readonly IDatabase _database = redis.GetDatabase();

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null) where T : class
    {
        var cacheData = await _database.StringGetAsync(key);
        if (!cacheData.IsNullOrEmpty)
            return JsonConvert.DeserializeObject<T>(cacheData);

        var data = await factory().ConfigureAwait(false);

        await _database.HashSetAsync(key,ObjectToHashEntry(data));
        _database.KeyExpire(key, expiration ?? TimeSpan.FromMinutes(30));

        return data;
    }

    public async Task RemoveCacheAsync(string key, CancellationToken token)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task RemoveByPatternAsync(string pattern)
    {
        var server = redis.GetServer(redis.GetEndPoints()[0]); // Lấy server đầu tiên
        //var keys = server.Keys(_database.Database, pattern: $"*{pattern}*");
        var keys = server.Keys(_database.Database, pattern: $"{pattern}*");

        foreach (var key in keys)
        {
            await _database.KeyDeleteAsync(key);
        }
    }

    public int RandomExpireTime(int minNum, int maxNum)
    {
        Random rnd = new Random();
        return rnd.Next(minNum, maxNum + 1);
    }

    private HashEntry[] ObjectToHashEntry<T>(T obj)
    {
        var properties = typeof(T).GetProperties();
        var hashEntry = properties.Select(c => new HashEntry(c.Name, c.GetValue(obj)?.ToString())).ToArray();

        return hashEntry;
    }

    public async Task RemoveCacheAsync(IDistributedCache cache, string key, CancellationToken token = default)
    {
        if (!string.IsNullOrEmpty(key))
            await cache.RemoveAsync(key, token).ConfigureAwait(false);
    }
}