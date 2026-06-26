namespace BuildingBlocks.Caching;

public class BaseCacheKey
{
    public string GetKeyCache(string id) => $"KT:{id}";
}