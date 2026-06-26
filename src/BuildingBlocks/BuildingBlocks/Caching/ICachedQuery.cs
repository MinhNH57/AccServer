namespace BuildingBlocks.Caching;

public interface ICachedQuery
{
    public string CacheKey { get; }
    public string TypeName { get;  }
    public TimeSpan Expiration { get; }
}