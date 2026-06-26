using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.HRM;

public static class Extensions
{
    public static IServiceCollection AddCatalogHrm(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(HrmRoot).Assembly;

        services.AddScoped<SmartDataServices>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddDbContext<HrmDbContext>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        //Async communication Services
        //services.AddMesageBroker(configuration);
        services.AddCustomMapster(typeof(HrmRoot).Assembly);

        return services;
    }
}