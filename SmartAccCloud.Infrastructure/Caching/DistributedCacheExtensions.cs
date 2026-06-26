using Newtonsoft.Json;

namespace SmartAccCloud.Infrastructure.Caching;

public static class DistributedCacheExtensions
{
    private static readonly SemaphoreSlim semaphore = new(1, 1);
    private static DistributedCacheEntryOptions DefaultExpriration => new DistributedCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
    };

    public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T?>> factory,
        DistributedCacheEntryOptions? options = null, CancellationToken token = default) where T : class
    {
        var cachedData = await cache.GetStringAsync(key, token).ConfigureAwait(false);

        if (cachedData is not null)
        {
            return JsonConvert.DeserializeObject<T>(cachedData)!;
        }

        //try
        //{
        //    var data = await factory().ConfigureAwait(false);
        //    if (data is not null)
        //        await cache.SetStringAsync(key, JsonConvert.SerializeObject(data), options ?? DefaultExpriration, token).ConfigureAwait(false);

        //    return data;
        //}
        //finally
        //{
        //    semaphore.Release();
        //}
        var data = await factory().ConfigureAwait(false);
        if (data is not null)
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(data), options ?? DefaultExpriration, token).ConfigureAwait(false);

        return data;
    }


    public static async Task RemoveCacheAsync(this IDistributedCache cache, string key, CancellationToken token = default)
    {
        if (!string.IsNullOrEmpty(key))
            await cache.RemoveAsync(key, token).ConfigureAwait(false);
    }

    public static int RandomExpireTime(this IDistributedCache cache, int minNum, int maxNum)
    {
        Random rnd = new Random();
        return rnd.Next(minNum, maxNum + 1);
    }

}