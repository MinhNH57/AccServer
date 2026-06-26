using Microsoft.AspNetCore.Http;
using Serilog.Events;

namespace BuildingBlocks.Logging;

public static class LogHelper
{
    public static LogEventLevel CustomGetLevel(HttpContext ctx, double _, Exception? ex) =>
        ex is not null
            ? LogEventLevel.Error
            : ctx.Response.StatusCode > 499
                ? LogEventLevel.Error
                : IsHealthCheckEndpoint(ctx)
                    ? LogEventLevel.Verbose
                    : LogEventLevel.Information;


    private static bool IsHealthCheckEndpoint(HttpContext ctx)
    {
        //var endpoint = ctx.Request.;
        //if (endpoint is not null) // same as !(endpoint is null)
        //{
        //    return string.Equals(
        //        endpoint.DisplayName,
        //        "/metrics",
        //        StringComparison.Ordinal);
        //}
        // No endpoint, so not a health check endpoint
        return false;
    }
}
