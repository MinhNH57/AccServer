using System.Reflection;
using System.Text;
using Finbuckle.MultiTenant;
using GenericServices.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartAccCloud.Application.Caching;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Interfaces.Multitenant;
using SmartAccCloud.Application.Models.Catalogs.CatalogUnit;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Application.Services.Dynamic;
using SmartAccCloud.Application.Services.Notifications;
using SmartAccCloud.Application.Services.User;
using SmartAccCloud.Infrastructure.Authentication;
using SmartAccCloud.Infrastructure.Caching;
using SmartAccCloud.Infrastructure.ConfigModel;
using SmartAccCloud.Infrastructure.Constants.ClaimType;
using SmartAccCloud.Infrastructure.Dynamic;
using SmartAccCloud.Infrastructure.Exception;
using SmartAccCloud.Infrastructure.Notification;
using SmartAccCloud.Infrastructure.Permissions;
using SmartAccCloud.Infrastructure.Persistence;
using SmartAccCloud.Infrastructure.Persistence.Dapper;
using SmartAccCloud.Infrastructure.Persistence.Multitenant;
using SmartAccCloud.Infrastructure.Tenants;
using StackExchange.Redis;


namespace SmartAccCloud.Infrastructure;

public static class DependenceInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationConfig(configuration);
        services.AddMultitenantConfig(configuration);
        services.AddDatabaseConfig();
        services.AddServices();
        services.AddCaching(configuration);

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddMemoryCache();
        services.AddDistributedMemoryCache();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddScoped<IPermissionnService, PermissionnService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRuleUnitService, RulesUnitService>();
        services.AddScoped<IDynamicCreateObjectServices, DynamicCreateObjectServices>();
        services.AddScoped<INotificationService, NotificationService>();
        //Add generic CRUD service
        services.GenericServicesSimpleSetup<ApplicationDbContext>(Assembly.GetAssembly(typeof(CatalogArea)), Assembly.GetAssembly(typeof(CatalogUnitVM)));
    }

    private static void AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(a =>
        {
            a.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
            };
        });

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<SwaggerSettings>(configuration.GetSection("SwaggerSettings"));
    }

    private static void AddDatabaseConfig(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDataServices, SmartDataServices>();
    }

    private static void AddMultitenantConfig(this IServiceCollection services, IConfiguration configuration)
    {

        #region CmtCode

        //services.AddDbContext<MultitenantStoreDbContext>(c =>
        //    {
        //        c.UseSqlServer(configuration.GetConnectionString("MultitenantConnection"))
        //         .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        //    })
        //    .AddMultiTenant<TenantInfoCustommize>()
        //    .WithHeaderStrategy("tenant")
        //    .WithClaimStrategy(ClaimTypeCustom.TenantId)
        //    .WithEFCoreStore<MultitenantStoreDbContext, TenantInfoCustommize>()
        //    .Services.AddScoped<IMultitenantService, MultitenantService>();

        #endregion

        services.AddDbContext<MasterDbContext>(c =>
            {
                c.UseSqlServer(configuration.GetConnectionString("MultitenantConnection"))
                    .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name], LogLevel.Information);
            })
            .AddMultiTenant<TenantInfoCustomize>()
            .WithStore<MultitenantStoreCustomize>(ServiceLifetime.Scoped)
            .WithHeaderStrategy(ClaimTypeCustom.TenantId)
            .WithClaimStrategy(ClaimTypeCustom.TenantId)
            .Services.AddScoped<IMultitenantService, TenantService>();
    }

    private static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(
        ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
        services.AddSingleton<RedisCacheService>();

        services.AddStackExchangeRedisCache(redisOpt =>
        {
            redisOpt.Configuration = configuration.GetConnectionString("Redis");
            //redisOpt.ConfigurationOptions = new ConfigurationOptions()
            //{
            //    AbortOnConnectFail = true,
            //    //EndPoints = { redisOpt.Configuration }
            //};
        });

        services.AddScoped<IRedisCacheService, RedisCacheService>();
    }


    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        builder.UseAuthentication();
        builder.UseAuthorization();

        return builder;
    }

}

