namespace SmartAccCloud.Application.Services.Notifications;

public interface INotificationService
{
    Task SendNotification(NotificationModel notification);
}