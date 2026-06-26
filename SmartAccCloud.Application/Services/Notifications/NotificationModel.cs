using FirebaseAdmin.Messaging;

namespace SmartAccCloud.Application.Services.Notifications;

public class NotificationModel
{
    public string Name { get; set; } = "SmartAccCloud";
    public string? Topic { get; set; }
    public Dictionary<string, string>? Data { get; set; }
    public AndroidConfig? Android { get; set; } = new() { Notification = new() { Priority = NotificationPriority.MAX } };
    public ApnsConfig? Apns { get; set; } = new()
    {
        Headers = new Dictionary<string, string>() { { "apns-priority", "10" } },
        Aps = new Aps { ContentAvailable = true }
    };
    //public NotificationData? DeviceNotification { get; set; }
    public FcmOptions? FcmOptions { get; set; }
    public WebpushConfig? WebpushConfig { get; set; }
    public Notification? Notification { get; set; }
}

public class NotificationData
{
    public string PackageName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string SubText { get; set; } = string.Empty;
    public long TimeStamp { get; set; }
}