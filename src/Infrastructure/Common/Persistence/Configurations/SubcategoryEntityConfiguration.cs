using Jobland.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobland.Infrastructure.Common.Persistence.Configurations;

public sealed class SubcategoryEntityConfiguration : IEntityTypeConfiguration<Subcategory>
{
    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        builder
            .ToTable("subcategory")
            .HasCharSet("utf8mb4")
            .HasComment("subcategory of work");

        builder.SetDefaultId();
        
        builder
            .Property(e => e.Name)
            .IsRequired(true)
            .HasMaxLength(255)
            .HasDefaultValue("");

        builder
            .HasOne(sc => sc.Category)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(sc => sc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.SetAuditableFields();
    }
}
