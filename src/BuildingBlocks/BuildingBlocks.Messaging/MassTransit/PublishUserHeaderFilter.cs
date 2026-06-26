using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Messaging.MassTransit;

public class PublishUserHeaderFilter<T>(IHttpContextAccessor httpContextAccessor, ILogger<PublishUserHeaderFilter<T>> logger) : IFilter<PublishContext<T>> where T : class
{
    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        var headers = httpContextAccessor.HttpContext.Request.Headers;
        headers.TryGetValue("X-Correlation-ID", out var correlationId);
        if (string.IsNullOrEmpty(correlationId))
        {
            logger.LogWarning("X-Correlation-ID was not found");
            return next.Send(context);
        }
        context.ConversationId = Guid.Parse(correlationId!.First()!);
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("publishuserheader");
    }
}