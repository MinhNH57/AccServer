using BuildingBlocks.Behaviors;
using BuildingBlocks.Mapster;
using BuildingBlocks.Messaging.MassTransit;
using FluentValidation;
using Ledger.API.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Ledger.API;

public static class LedgerModule
{
    public static IServiceCollection AddLedgerModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(Program).Assembly;
        services.AddDbContext<LedgerDbContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddCustomMapster(typeof(LedgerRoot).Assembly);

        services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.PropertyNamingPolicy = null;
            o.SerializerOptions.DictionaryKeyPolicy = null;
            o.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assembly);

        services.AddHttpContextAccessor();

        services.AddMesageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}