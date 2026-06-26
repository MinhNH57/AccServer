using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Messaging.MassTransit;
using BuildingBlocks.Web;
using Catalog.Fund.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Fund;

public static class Extensions
{
    public static IServiceCollection AddCatalogFund(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(FundRoot).Assembly;

        services.AddScoped<SmartDataServices>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddDbContext<CatalogFundContext>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        //Async communication Services
        services.AddMesageBroker(configuration);
        services.AddCustomMapster(typeof(FundRoot).Assembly);

        return services;
    }
}