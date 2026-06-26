using Refit;

namespace Voucher.Acc.ExternalApis;

public class NotificationRequest
{
    public string Title { get; set; } = "Title";
    public string Body { get; set; } = "Body";
    public IReadOnlyList<string>? Tokens { get; set; }
}

public interface INotificationService
{
    [Post("/push-notification-multicast")]
    Task PushNotification(NotificationRequest request);
}