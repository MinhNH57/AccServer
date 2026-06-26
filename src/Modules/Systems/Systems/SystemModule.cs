using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Web;
using Systems.Infrastructure;

namespace Systems;

public static class SystemModule
{
    public static IServiceCollection AddSystemModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(Program).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        services.AddCustomMapster(typeof(Program).Assembly);

        services.AddDbContext<SystemDbcontext>();
        services.AddSingleton<SmartDataServices>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        return services;
    }

}