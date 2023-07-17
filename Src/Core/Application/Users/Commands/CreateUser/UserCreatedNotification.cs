using Application.Common.Interfaces;
using Application.Common.Notification.Models;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class UserCreatedNotification : INotification
{
    public string UserId { get; set; }

    public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedNotification>
    {
        private readonly INotificationService _notificationService;

        public UserCreatedNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _notificationService.SendAsync(new NotifMessageDto
            {

                Body = $"User with Id:\"{notification.UserId}\" created Successfully."
            },
                cancellationToken);
        }


    }
}