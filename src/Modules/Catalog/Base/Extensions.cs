using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Base;

public static class Extensions
{
    public static IServiceCollection AddCatalogBase(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DataRoot).Assembly;

        services.AddScoped<SmartDataServices>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddDbContext<CatalogDbContext>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        //Async communication Services
        //services.AddMesageBroker(configuration);
        services.AddCustomMapster(typeof(DataRoot).Assembly);

        return services;
    }
}