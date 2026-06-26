using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Web;
using Financial.Infrastructure;
using FluentValidation;
using CurrentUser = Financial.Model.CurrentUser;

namespace Financial;

public static class FinancialModule
{
    public static IServiceCollection AddFinancialModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(Program).Assembly;
        services.AddDbContext<FinancialDbContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<SmartDataServices>();

        services.AddCustomMapster(assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assembly);

            
        services.AddHttpContextAccessor();

        return services;
    }
}