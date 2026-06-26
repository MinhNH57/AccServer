using FirebaseAdmin.Messaging;
using Newtonsoft.Json;
using SmartAccCloud.Application.Services.Notifications;

namespace SmartAccCloud.Infrastructure.Notification;

public class NotificationService : INotificationService
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

    private Dictionary<string, string>? ConvertToDictionary(object? obj)
    {
        if (obj == null) return null;

        //var dictionary = new Dictionary<string, string>();
        //foreach (var prop in obj.GetType().GetProperties())
        //{
        //    var value = prop.GetValue(obj)?.ToString() ?? string.Empty;
        //    dictionary.Add(prop.Name, value);
        //}
        string json = JsonConvert.SerializeObject(obj);
        var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        return result;
        //return dictionary;
    }
}