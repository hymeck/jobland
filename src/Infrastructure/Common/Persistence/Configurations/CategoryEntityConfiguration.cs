using Jobland.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobland.Infrastructure.Common.Persistence.Configurations;

public sealed class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("category")
            .HasCharSet("utf8mb4")
            .HasComment("category of work");
        
        builder.SetDefaultId();
        
        builder
            .Property(e => e.Name)
            .IsRequired(true)
            .HasMaxLength(255)
            .HasDefaultValue("");
        
        builder
            .Property(e => e.IconUrl)
            .IsRequired(true)
            .HasMaxLength(255)
            .HasDefaultValue("");
        
        
        builder.SetAuditableFields();
    }
}
