using Application.Common.Interfaces;
using Application.Common.Notification.Models;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CustomerCreatedNotification : INotification
{
    public string CustomerId { get; set; }

    public class CustomerCreatedNotificationHandler : INotificationHandler<CustomerCreatedNotification>
    {
        private readonly INotificationService _notificationService;

        public CustomerCreatedNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(CustomerCreatedNotification notification)
        {
            _notificationService.SendAsync(new NotifMessageDto
            {

                Body = $"Customer with Id:\"{CustomerId}\" created Successfully"
            });
        }
    }
}