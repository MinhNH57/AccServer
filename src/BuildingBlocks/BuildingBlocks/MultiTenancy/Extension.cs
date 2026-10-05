using BuildingBlocks.Permission;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.MultiTenancy;

public static class Extension
{
    public static IServiceCollection AddMultiTenancy(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MasterDbContext>(c =>
            {
                c.UseSqlServer(configuration.GetConnectionString("MultitenantConnection"))
                    .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name], LogLevel.Information);
            })
            .AddMultiTenant<TenantInfoCustomize>()
            .WithDistributedCacheStore()
            .WithStore<MultitenantStoreCustomize>(ServiceLifetime.Scoped)
            .WithHeaderStrategy(TenantConstant.TenantIdHeader)
            .WithClaimStrategy(ClaimTypeCustom.TenantId)
            .Services.AddStackExchangeRedisCache(opt =>
            {
                var connectionString = configuration.GetConnectionString("Redis")
                    ?? throw new InvalidOperationException("ConnectionStrings:Redis is not configured.");
                opt.ConfigurationOptions = StackExchange.Redis.ConfigurationOptions.Parse(connectionString);
                opt.ConfigurationOptions.AbortOnConnectFail = false;
            });

        services.AddHealthChecks().AddSqlServer(
            configuration.GetConnectionString("MultitenantConnection")
                ?? throw new InvalidOperationException("ConnectionStrings:MultitenantConnection is not configured."),
            name: "tenant-database", timeout: TimeSpan.FromSeconds(5));

        return services;
    }

    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
    {
        app.UseMultiTenant();
        return app;
    }

}
