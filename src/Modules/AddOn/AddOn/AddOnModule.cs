using AddOn.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Web;

namespace AddOn;

public static class AddOnModule
{
    public static IServiceCollection AddAddOnModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<SmartDataServices>();
        services.AddDbContext<AddOnDbContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        var assembly = typeof(Program).Assembly;
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        return services;
    }
}