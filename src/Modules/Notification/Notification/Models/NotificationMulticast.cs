namespace Notification.Models;

public class NotificationMulticast
{
    public string Title { get; set; } = "Title";
    public string Body { get; set; } = "Body";
    public Dictionary<string, string>? Data { get; set; }
    public IReadOnlyList<string>? Tokens { get; set; }
}