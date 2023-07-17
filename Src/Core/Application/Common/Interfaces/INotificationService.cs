using Application.Common.Notification.Models;

namespace Application.Common.Interfaces;

public interface INotificationService
{
    Task SendAsync(NotifMessageDto message,CancellationToken cancellationToken);
}