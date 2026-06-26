using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Messaging.MassTransit;
using Notification.Data;
using System.Reflection;
using BuildingBlocks.Web;

namespace Notification;

public static class NotificationModule
{
    public static IServiceCollection AddNotificationModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = typeof(Program).Assembly;

        services.AddScoped<SmartDataServices>();
        services.AddDbContext<NotificationDbContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        services.AddCustomMapster(typeof(NotificationModule).Assembly);

        //Async communication Services
       
        //services.AddMesageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}