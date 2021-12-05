using Jobland.Infrastructure.Common.Messenger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobland.Infrastructure.Common.Persistence.Configurations;

public sealed class DirectMessageEntityConfiguration : IEntityTypeConfiguration<DirectMessage>
{
    public void Configure(EntityTypeBuilder<DirectMessage> builder)
    {
        builder
            .ToTable("directmessages")
            .HasCharSet("utf8mb4")
            .HasComment("direct messages from one user to other user");
        
        builder.SetDefaultId();
        
        builder
            .Property(e => e.TextBody)
            .IsRequired(true)
            .HasMaxLength(65535)
            .HasDefaultValue("");

        builder
            .HasOne(pi => pi.Sender)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(pi => pi.Receiver)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.SetAuditableFields();
    }
}
