
using Application.Common.Interfaces;
using Common;
using Infrastructure.DateTimeService;
using Infrastructure.Notification;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
       
        services.AddTransient<IDateTime, MachineDateTime>();
        services.AddTransient<INotificationService, NotificationService>();


        return services;
    }
}