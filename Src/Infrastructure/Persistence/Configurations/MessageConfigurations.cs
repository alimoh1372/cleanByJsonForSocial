using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MessageConfigurations:IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id).HasColumnName("MessageID");
        builder.Property(x => x.MessageContent).IsRequired().HasColumnType("ntext");
        

        //Define many-to-many self-relation with user
        builder.HasOne(x => x.FromUser)
            .WithMany(x => x.FromMessages)
            .HasForeignKey(x => x.FkFromUserId)
            .OnDelete(DeleteBehavior.NoAction)
            ;

        builder.HasOne(x => x.ToUser)
            .WithMany(x => x.ToMessages)
            .HasForeignKey(x => x.FkToUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}