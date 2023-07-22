using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Persistence.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId).HasColumnName("UserID").UseIdentityColumn(1);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(255).HasColumnName("Last Name");
        builder.Property(x => x.Email).IsRequired().HasColumnType("nvarchar").HasMaxLength(1000).HasColumnName("Email/User name");
        builder.Property(X => X.ProfilePicture).HasColumnType("image");
        builder.Property(x => x.BirthDay).HasColumnType("datetime");
        builder.Property(x => x.AboutMe).HasColumnType("ntext");
        

       //Define self-reference many-to-many user with relation

       builder.HasMany(x => x.UserARelations)
           .WithOne(x => x.UserA)
           .HasForeignKey(x => x.FkUserAId)
           .OnDelete(DeleteBehavior.NoAction);

       builder.HasMany(x => x.UserBRelations)
           .WithOne(x => x.UserB)
           .HasForeignKey(x => x.FkUserBId)
           .OnDelete(DeleteBehavior.NoAction);


       //Define self-reference many-to-many user with message

       builder.HasMany(x => x.FromMessages)
           .WithOne(x => x.FromUser)
           .HasForeignKey(x => x.FkFromUserId)
           .OnDelete(DeleteBehavior.NoAction);

       builder.HasMany(x => x.ToMessages)
           .WithOne(x => x.ToUser)
           .HasForeignKey(x => x.FkToUserId)
           .OnDelete(DeleteBehavior.NoAction);


       //Define indexes

       builder.HasIndex(x => x.Email)
           .IsUnique();



    }
}