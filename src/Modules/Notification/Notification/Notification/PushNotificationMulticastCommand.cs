using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using FirebaseAdmin.Messaging;
using Notification.Models;
using Notification.Service;
using Serilog;

namespace Notification.Notification;

public class NotificationMulticast : BaseNotificationModel
{
    public IReadOnlyList<string>? Tokens { get; set; }
}

public record PushMulticastNotificationCommand(NotificationMulticast Message) : ICommand<Result>;


public class PushMulticastNotificationCommandHandler : ICommandHandler<PushMulticastNotificationCommand, Result>
{
    public async Task<Result> Handle(PushMulticastNotificationCommand command, CancellationToken cancellationToken)
    {
        var message = new FireBaseMulticastBuilder(command.Message.Tokens!)
            .WithAndroid()
            .WithApns()
            .WithNotification(command.Message.Title, command.Message.Body)
            .WithData(command.Message.Data)
            .Build();

        var result = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message, cancellationToken);
        var responseFireBase = result.Responses;
        foreach (var item in responseFireBase)
        {
            if (item.IsSuccess)
                Log.Information($"{item.MessageId} send successfully");
            else
            {
                Log.Warning($"{item.MessageId} send failed: {item.Exception}");
            }
        }
        
        return Result.Success();
    }
}