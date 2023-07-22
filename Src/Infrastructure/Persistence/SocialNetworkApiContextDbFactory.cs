using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class SocialNetworkApiContextDbFactory:DesignTimeDbContextFactoryBase<SocialNetworkApiContext>
{
    protected override SocialNetworkApiContext CreateNewInstance(DbContextOptions<SocialNetworkApiContext> options)
    {
        return new SocialNetworkApiContext(options);
    }
}