using BuildingBlocks.Behaviors;
using BuildingBlocks.Mapster;
using BuildingBlocks.Messaging.MassTransit;
using BuildingBlocks.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Voucher.Acc;

public static class Extensions
{
    public static IServiceCollection AddVoucherAcc(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(VoucherRoot).Assembly;

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        //Async communication Services
        //services.AddMesageBroker(configuration);
        services.AddCustomMapster(typeof(VoucherRoot).Assembly);

        return services;
    }
}