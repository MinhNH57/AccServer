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
                opt.Configuration = configuration.GetConnectionString("Redis");
            });

        return services;
    }

    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
    {
        app.UseMultiTenant();
        return app;
    }

}