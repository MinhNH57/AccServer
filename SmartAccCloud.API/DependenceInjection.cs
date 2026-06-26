using Microsoft.Extensions.Options;
using SmartAccCloud.API.Config;
using SmartAccCloud.Domain.Validation;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartAccCloud.API;

public static class DependenceInjection
{
    public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersionConfig();
        services.AddCorsConfig();
        services.AddServices(configuration);
        return services;
    }

    private static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Custom response validation
        services.Configure<ApiBehaviorOptions>(x =>
            x.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult());

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

    }

    private static void AddApiVersionConfig(this IServiceCollection services)
    {
        //Add Version API
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    private static void AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(opts =>
        {
            opts.AddPolicy(name: "SMARTAccCloud", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });
    }
}

public static class UseResgister
{
    public static IApplicationBuilder UserApiServices(this IApplicationBuilder builder)
    {
        builder.UseCorsConfig();
        return builder;

    }

    public static void UseCorsConfig(this IApplicationBuilder builder)
    {
        builder.UseCors("SMARTAccCloud");
    }
}