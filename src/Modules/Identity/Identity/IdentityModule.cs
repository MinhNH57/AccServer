using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Identity.Services;
using CurrentUser = Identity.Services.CurrentUser;

namespace Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(Program).Assembly;

        services.AddDbContext<IdentityDbContext>();
        services.AddCustomMapster(typeof(IdentityRoot).Assembly);

        services.AddSingleton<SmartDataServices>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}