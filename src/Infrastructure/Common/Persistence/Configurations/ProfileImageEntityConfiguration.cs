using Jobland.Infrastructure.Common.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobland.Infrastructure.Common.Persistence.Configurations;

public sealed class ProfileImageEntityConfiguration : IEntityTypeConfiguration<ProfileImage>
{
    public void Configure(EntityTypeBuilder<ProfileImage> builder)
    {
        builder
            .ToTable("profileimages")
            .HasCharSet("utf8mb4")
            .HasComment("user profile images");
        
        builder.SetDefaultId();
        
        builder
            .Property(e => e.ImageUrl)
            .IsRequired(true)
            .HasMaxLength(255)
            .HasDefaultValue("");

        builder
            .HasOne(pi => pi.Owner)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.SetAuditableFields();
    }
}
