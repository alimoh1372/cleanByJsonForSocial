using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SocialNetworkApiContext>
        (
            option => option.UseSqlServer(configuration.GetConnectionString("SocialApiConnectionString")
            )
        );
        services.AddScoped<ISocialNetworkDbContext>(provider => provider.GetService<SocialNetworkApiContext>()!);
        return services;
    }
}