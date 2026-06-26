using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Messaging.MassTransit;
using FluentValidation;
using Voucher.Acc;
using Voucher.Acc.Infrastructure;
using Voucher.HRM.Infrastructure;
using Voucher.Sgas.Infrastructure;

namespace Voucher;

public static class VoucherModule
{
    public static IServiceCollection AddVoucherModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(Program).Assembly;
        services.AddDbContext<VoucherDbContext>();
        services.AddDbContext<VoucherSgasDbContext>();
        services.AddDbContext<VoucherHrmDbContext>();
        services.AddScoped<SmartDataServices>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddCustomMapster(typeof(VoucherRoot).Assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assembly);


        services.AddMesageBroker(configuration);
        services.AddHttpContextAccessor();

        return services;
    }
}