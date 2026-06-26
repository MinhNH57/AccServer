using BuildingBlocks.Caching;

namespace BuildingBlocks.CQRS;

public interface ICacheQuery<out TResponse> : IQuery<TResponse>, ICachedQuery
    where TResponse : class
{

}