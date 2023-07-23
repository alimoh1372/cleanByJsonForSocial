using System.Reflection;
using Application.Common.Behavior;
using Application.Common.Interfaces;
using Application.Common.Utility;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddScoped<IUserAndUserRelationService, UserAndUserRelationServices>();

        return services;
    }
}