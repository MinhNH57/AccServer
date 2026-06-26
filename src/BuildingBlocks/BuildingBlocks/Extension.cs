using BuildingBlocks.Caching;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Jwt;
using BuildingBlocks.Logging;
using BuildingBlocks.Middleware;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.OpenApi;
using BuildingBlocks.Web;
using Figgle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;

namespace BuildingBlocks;

public static class Extension
{
    public static WebApplicationBuilder AddDefaultService(this WebApplicationBuilder builder,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");
        if (appOptions is null) throw new SmartException("AppOptions not configured");
        Console.WriteLine(FiggleFonts.Standard.Render(appOptions.Name));

        builder.AddCustomSerilog(env);

        builder.Services.AddMultiTenancy(configuration);
        //builder.Services.AddCustomMapster(assembly);
        builder.AddDefaultHealthChecks();

        builder.Services.AddJwt();

        builder.Services.AddCustomVersioning();

        builder.Services.AddAspnetOpenApi();

        builder.Services.AddCors();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddProblemDetails();

        //builder.ConfigureOpenTelemetry();

        builder.Services.AddCaching(configuration);
        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });
        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    //.AddOtlpExporter(use =>
                    //{
                    //    //use.Endpoint = new Uri("http://localhost:9090/api/v1/otlp/v1/metrics");
                    //    use.Endpoint = new Uri("http://localhost:4318/v1/metrics");
                    //    use.Protocol = OtlpExportProtocol.HttpProtobuf;
                    //});
                    .AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    // We want to view all traces in development
                    tracing.SetSampler(new AlwaysOnSampler());
                }

                tracing.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
            });

        return builder;

    }

    public static WebApplication MapDefaultMiddlware(this WebApplication app)
    {
        app.UseSerilogRequestLogging(opt =>
        {
            opt.EnrichDiagnosticContext = LogEnrichHelper.EnrichFromRequest;
            opt.GetLevel = LogHelper.CustomGetLevel;
        });
        app.UseExceptionHandler();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        //app.UseMiddleware<CorrelationIdMiddlware>();
        app.UseMiddleware<CurrentUserMiddlware>();
        app.UseMultiTenancy();
        // app.UseOpenTelemetryPrometheusScrapingEndpoint();
        return app;
    }


    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Uncomment the following line to enable the Prometheus endpoint (requires the OpenTelemetry.Exporter.Prometheus.AspNetCore package)
        // app.MapPrometheusScrapingEndpoint();

        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            //Api document
            app.UseAspnetOpenApi();

        }
        // All health checks must pass for app to be considered ready to accept traffic after starting
        app.MapHealthChecks("/health");

        // Only health checks tagged with the "live" tag must pass for app to be considered alive
        app.MapHealthChecks("/alive", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live")
        });

        return app;
    }

}