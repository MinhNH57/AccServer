using BuildingBlocks.Behaviors;
using BuildingBlocks.Web;
using CMS.Infrastructure;

namespace CMS;

public static class CmsModule
{
    public static IServiceCollection AddCmsModule(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;

        services.AddDbContext<CmsDbContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        return services;
    }
}