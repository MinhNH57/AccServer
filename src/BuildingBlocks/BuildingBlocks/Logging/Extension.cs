using System.Text;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Sinks.SpectreConsole;

namespace BuildingBlocks.Logging;

public static class Extension
{
    public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder builder, IWebHostEnvironment env)
    {
        builder.Host.UseSerilog((context, services, loggerConfig) =>
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var appOption = context.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();
            var logOptions = context.Configuration.GetSection(nameof(LogOptions)).Get<LogOptions>();

            var logLevel = Enum.TryParse<LogEventLevel>(logOptions.Level, true, out var level)
                ? level
                : LogEventLevel.Information;

            loggerConfig
                .MinimumLevel.Is(logLevel)
                 .WriteTo.SpectreConsole(logOptions.LogTemplate, logLevel)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                //Thêm cái này sẽ bị Memory lead trùng key EF core chưa biết tại sao
                //.Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", $"{appOption.Name}")
                .ReadFrom.Configuration(context.Configuration);

            loggerConfig.WriteTo.Async(c =>
            {

                if (logOptions.FileOption is not { Enabled: true })
                {
                    return;
                }
                var root = env.ContentRootPath;
                //TODO: Thêm ghi log theo Tenant-Id

                //var tenantId = currentUser.TenantId ?? "default"; // Sử dụng TenantId từ ICurrentUser, nếu không có thì dùng "default"
                //var logFolderPath = Path.Combine(root, "logs", tenantId); // Thư mục dựa trên TenantId

                Directory.CreateDirectory(Path.Combine(root, "logs"));

                var path = string.IsNullOrWhiteSpace(logOptions.FileOption.Path) ? "logs/.txt" : logOptions.FileOption.Path;
                if (!Enum.TryParse<RollingInterval>(logOptions.FileOption.Interval, true, out var interval))
                {
                    interval = RollingInterval.Day;
                }

                var retainedFile = logOptions.FileOption.RetainedFileCountLimit;
                // loggerConfig.WriteTo.File(path, rollingInterval: interval, encoding: Encoding.UTF8, outputTemplate: logOptions.LogTemplate, retainedFileCountLimit: retainedFile);
                c.File(path, rollingInterval: interval, encoding: Encoding.UTF8, outputTemplate: logOptions.LogTemplate, 
                    buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(2),
                    retainedFileCountLimit: retainedFile);

            });
            // if (logOptions.SeqOption is { Enable: true })
            // {
            //     loggerConfig.WriteTo.Seq(serverUrl: logOptions.SeqOption.Url);
            // }

            loggerConfig.Filter.ByExcluding(Matching.WithProperty<string>("RequestPath",
            p => p.StartsWith("/metrics")));

            loggerConfig.Filter.ByExcluding(Matching.WithProperty<string>("RequestPath",
            p => p.StartsWith("/health")));
        });

        return builder;
    }
}