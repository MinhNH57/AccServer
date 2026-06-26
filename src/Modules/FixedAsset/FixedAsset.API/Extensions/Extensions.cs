using BuildingBlocks.Web;
using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;
using System.Text.Json.Serialization;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        // Add the authentication services to DI
        //builder.AddDefaultAuthentication();

        // Pooling is disabled because of the following error:
        // Unhandled exception. System.InvalidOperationException:
        // The DbContext of type 'FixedAssetDbContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
        services.AddDbContext<FixedAssetDbContext>();

        services.AddHttpContextAccessor();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.PropertyNamingPolicy = null;
            o.SerializerOptions.DictionaryKeyPolicy = null;
            o.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        // Configure mediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        // Register the command validators for the validator behavior (validators based on FluentValidation library)
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        // Configure mapster;
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddScoped<IFixedAssetQueries, FixedAssetQueries>();
        services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
        services.AddScoped<IFADecrementRepository, FADecrementRepository>();
        services.AddScoped<IFATransferRepository, FATransferRepository>();
        services.AddScoped<IFADepreciationRepository, FADepreciationRepository>();
        services.AddScoped<IFAAuditRepository, FAAuditRepository>();
        services.AddScoped<IFAAdjustmentRepository, FAAdjustmentRepository>();
        services.AddScoped<IFAChangeFinancialLeasingToOwnerRepository, FAChangeFinancialLeasingToOwnerRepository>();
        services.AddScoped<IRequestManager, RequestManager>();

        services.AddMesageBroker(builder.Configuration, Assembly.GetExecutingAssembly());
    }
}
