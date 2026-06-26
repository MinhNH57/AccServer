using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BuildingBlocks.Caching;

public static class Extension
{
    public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
        services.AddSingleton<RedisCacheService>();

        services.AddStackExchangeRedisCache(redisOpt =>
        {
            redisOpt.Configuration = configuration.GetConnectionString("Redis");
            //redisOpt.ConfigurationOptions = new ConfigurationOptions()
            //{
            //    AbortOnConnectFail = true,
            //    //EndPoints = { redisOpt.Configuration }
            //};
        });

        services.AddScoped<IRedisCacheService, RedisCacheService>();
    }
}