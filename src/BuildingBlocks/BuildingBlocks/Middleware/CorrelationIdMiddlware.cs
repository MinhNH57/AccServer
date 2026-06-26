using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Middleware;

public class CorrelationIdMiddlware(RequestDelegate next)
{
    private const string Key = "X-Correlation-ID";

    public async Task Invoke(HttpContext context)
    {
        context.Request.Headers.TryGetValue(Key, out var correlationId);

        if (correlationId.FirstOrDefault() is null)
        {
            correlationId = Guid.NewGuid().ToString();
            context.Request.Headers.Append(Key, correlationId);
        }

        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Append(Key, correlationId);
            return Task.CompletedTask;
        });

        await next(context);
    }
}