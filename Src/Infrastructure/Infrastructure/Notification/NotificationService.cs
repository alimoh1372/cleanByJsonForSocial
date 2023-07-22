using Application.Common.Interfaces;
using Application.Common.Notification.Models;

namespace Infrastructure.Notification;

public class NotificationService:INotificationService
{
    public Task SendAsync(NotifMessageDto message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}