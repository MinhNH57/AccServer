using BuildingBlocks.Messaging.Events;
using FileHandle.Models;
using FileHandle.Services;
using MassTransit;

namespace FileHandle.EventHandlers;

public class DeleteFileEventHandler(IFileAttachServices fileServices, ILogger<DeleteFileEventHandler> logger) : IConsumer<DeleteFileEvent>
{
    public async Task Consume(ConsumeContext<DeleteFileEvent> context)
    {
        logger.LogInformation("Integration Event handled: {0}", context.Message.GetType().Name);
        var query = new QueryFile() { ColumnTable = context.Message.ColumnTable, KeyTable = context.Message.KeyTable };

        await fileServices.DeleteAllFileFund(query, context.Headers.Get<string>("X-Tenant-Id"));
       //await context.RespondAsync(isSuccess);
    }
}