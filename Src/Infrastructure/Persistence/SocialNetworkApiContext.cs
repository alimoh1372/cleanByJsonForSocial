using Application.Common.Interfaces;
using Common;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class SocialNetworkApiContext : DbContext, ISocialNetworkDbContext
{
    private readonly IAuthHelper _authHelper;
    private readonly IDateTime _dateTime;
    


    public SocialNetworkApiContext(DbContextOptions<SocialNetworkApiContext> options) : base(options)
    {

    }

    public SocialNetworkApiContext(DbContextOptions<SocialNetworkApiContext> options
        , IAuthHelper authHelper
        , IDateTime dateTime)
        : base(options)
    {
        _authHelper = authHelper;
        _dateTime = dateTime;
    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRelation> UserRelations { get; set; }
    public DbSet<Message> Messages { get; set; }

    public override  Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var user = _authHelper.GetUserInfo().Result;
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = _dateTime.Now;
                entry.Entity.CreatedBy =user.Id.ToString();
            }

            if (entry.State==EntityState.Modified)
            {
                entry.Entity.LastModified=_dateTime.Now;
                entry.Entity.LastModifiedBy = user.Id.ToString();
            }
        }

        return  base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialNetworkApiContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}