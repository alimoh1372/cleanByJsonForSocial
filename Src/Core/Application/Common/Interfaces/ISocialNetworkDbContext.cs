using Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace Application.Common.Interfaces;

public interface ISocialNetworkDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRelation> UserRelations { get; set; }

    public DbSet<Message> Messages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}