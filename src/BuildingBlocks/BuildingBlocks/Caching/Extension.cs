using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BuildingBlocks.Caching;

public static class Extension
{
    public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        var connectionString = configuration.GetConnectionString("Redis")
            ?? throw new InvalidOperationException("ConnectionStrings:Redis is not configured.");
        var redisOptions = ConfigurationOptions.Parse(connectionString);
        redisOptions.AbortOnConnectFail = false;
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(redisOptions));
        services.AddSingleton<RedisCacheService>();

        services.AddStackExchangeRedisCache(redisOpt =>
        {
            redisOpt.ConfigurationOptions = redisOptions;
        });

        services.AddHealthChecks().AddRedis(connectionString, name: "redis",
            timeout: TimeSpan.FromSeconds(5));

        services.AddScoped<IRedisCacheService, RedisCacheService>();
    }
}
