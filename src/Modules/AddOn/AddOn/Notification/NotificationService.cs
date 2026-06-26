using FirebaseAdmin.Messaging;

namespace AddOn.Notification;


public class NotificationService 
{
    public async Task SendNotification(NotificationModel notification)
    {
        var message = new Message()
        {
            Apns = notification.Apns,
            Android = notification.Android,
            FcmOptions = notification.FcmOptions,
            Webpush = notification.WebpushConfig,
            Notification = notification.Notification,
            Topic = notification.Topic,
            Data = notification.Data,
        };
        var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }

}