using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using FirebaseAdmin.Messaging;
using Notification.Models;
using Notification.Service;
using Serilog;

namespace Notification.Notification;

public class NotificationTopic : BaseNotificationModel
{
    public string Topic { get; set; }
}

public record PushNotificationTopicCommand(NotificationTopic Message) : ICommand<Result>;

public class PushNotificationTopicCommandHandler : ICommandHandler<PushNotificationTopicCommand, Result>
{
    public async Task<Result> Handle(PushNotificationTopicCommand command, CancellationToken cancellationToken)
    {
        var message = new FireBaseMessagBuilder(command.Message.Topic)
            .WithAndroid()
            .WithApns()
            .WithNotification(command.Message.Title, command.Message.Body)
            .WithData(command.Message.Data)
            .Build();
        var result = await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);
        Log.Information(result);
        return Result.Success();
    }
}