using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Web;
using Report.Infrastructure;

namespace Report;

public static class ReportModule
{
    public static IServiceCollection AddReportModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(Program).Assembly;

        services.AddScoped<SmartDataServices>();
        services.AddDbContext<ReportDbContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        // services.AddScoped<IDynamicCreateObjectServices, DynamicCreateObjectServices>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddCustomMapster(typeof(ReportModule).Assembly);

        //Async communication Services
        //services.AddMesageBroker(configuration);

        return services;    
    }
}