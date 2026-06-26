namespace Notification.Models;

public abstract class BaseNotificationModel
{
    public string Title { get; set; } = "Title";
    public string Body { get; set; } = "Body";
    public Dictionary<string, string>? Data { get; set; }
}