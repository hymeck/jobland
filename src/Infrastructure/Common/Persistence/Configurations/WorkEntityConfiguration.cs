using Jobland.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobland.Infrastructure.Common.Persistence.Configurations;

public sealed class WorkEntityConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder
            .ToTable("work")
            .HasCharSet("utf8mb4")
            .HasComment("work request in public employment service");

        builder.SetDefaultId();

        builder
            .Property(e => e.Title)
            .IsRequired(true)
            .HasMaxLength(255)
            .HasDefaultValue("");
        
        builder
            .Property(e => e.Description)
            .IsRequired(true)
            .HasMaxLength(65535)
            .HasDefaultValue("");
        
        builder
            .Property(e => e.StartedOn)
            .IsRequired(false);
        
        builder
            .Property(e => e.FinishedOn)
            .IsRequired(false);
        
        builder
            .Property(e => e.PhoneNumber)
            .IsRequired(true)
            .HasMaxLength(30)
            .HasDefaultValue("");
        
        builder
            .Property(e => e.LowerPriceBound)
            .IsRequired(false);
        
        builder
            .Property(e => e.UpperPriceBound)
            .IsRequired(false);
        
        builder
            .Property(e => e.ResponseCount)
            .IsRequired(true)
            .HasDefaultValue(0L);

        builder
            .HasOne(w => w.Subcategory)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.SetAuditableFields();
    }
}
