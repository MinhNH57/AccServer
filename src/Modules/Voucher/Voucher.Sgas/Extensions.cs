using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.SmartMapper;
using BuildingBlocks.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Voucher.Sgas;

public static class Extensions
{
    public static IServiceCollection AddVoucherSgas(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(VoucherSgasRoot).Assembly;

        services.AddScoped<ICurrentUser, CurrentUser>(); 
        services.AddScoped<IMappingService, MappingService>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        //Async communication Services
        //services.AddMesageBroker(configuration);
        services.AddCustomMapster(typeof(VoucherSgasRoot).Assembly);

        return services;
    }
}