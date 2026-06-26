using System.Text.Json;
using BuildingBlocks.Caching;
using BuildingBlocks.Response;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class QueryCachingBehavior<TRequest, TResponse>(
    IDistributedCache cache,
    ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result

{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheResult = await cache.GetDataCacheAsync<TResponse>(request.CacheKey, cancellationToken);
        if (cacheResult is not null)
        {
            logger.LogInformation("Cache hit  for {RequestName}", typeof(TRequest).Name);
            return cacheResult;
        }
        logger.LogInformation("Cache miss  for {RequestName}", typeof(TRequest).Name);
        TResponse result = await next();
        if (result.IsSuccess)
        {
            await cache.SetStringAsync(request.CacheKey, JsonSerializer.Serialize(result), new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = request.Expiration,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            }, token: cancellationToken);
        }

        return result;
    }
    //public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}