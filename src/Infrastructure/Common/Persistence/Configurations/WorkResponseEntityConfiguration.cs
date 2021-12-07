using Jobland.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobland.Infrastructure.Common.Persistence.Configurations;

public sealed class WorkResponseEntityConfiguration : IEntityTypeConfiguration<WorkResponse>
{
    public void Configure(EntityTypeBuilder<WorkResponse> builder)
    {
        builder
            .ToTable("workresponse")
            .HasCharSet("utf8mb4")
            .HasComment("which user responded to work");

        builder.SetDefaultId();
        
        builder
            .Property(e => e.ResponderId)
            .IsRequired(true)
            .HasMaxLength(255)
            .HasDefaultValue("");

        builder
            .HasOne(wr => wr.Work)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.SetAuditableFields();
    }
}
