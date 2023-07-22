using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserRelationConfiguration:IEntityTypeConfiguration<UserRelation>
{
    public void Configure(EntityTypeBuilder<UserRelation> builder)
    {
        builder.ToTable("UserRelations");
        builder.HasKey(x => x.UserRelationId);

        builder.Property(x => x.UserRelationId).UseIdentityColumn(1);
        builder.Property(x => x.Approve).HasDefaultValue(false);
        builder.Property(x => x.RelationRequestMessage).HasColumnType("nvarchar").HasMaxLength(30);


        //Define many-to-many self-referencing 
        builder.HasOne(x => x.UserA)
            .WithMany(x => x.UserARelations)
            .HasForeignKey(x => x.FkUserAId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.UserB)
            .WithMany(x => x.UserBRelations)
            .HasForeignKey(x => x.FkUserBId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}