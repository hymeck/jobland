using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Mysql
{
    public sealed class WorkTypeConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder
                .ToTable("work")
                .HasCharSet("utf8mb4");

            builder
                .HasKey(e => e.Id);
            
            builder
                .Property(e => e.Id)
                .HasColumnName("work_id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(e => e.Description)
                .HasColumnName("desc")
                .HasMaxLength(65535)
                .IsRequired();

            builder
                .Property(e => e.StartedOn)
                .HasColumnName("started")
                .IsRequired(false)
                .HasColumnType("datetime");
            
            builder
                .Property(e => e.FinishedOn)
                .HasColumnName("finished")
                .IsRequired(false)
                .HasColumnType("datetime");

            builder
                .Property(e => e.LowerPriceBound)
                .HasColumnName("lower_price")
                .IsRequired(false);
            
            builder
                .Property(e => e.UpperPriceBound)
                .HasColumnName("upper_price")
                .IsRequired(false);
            
            builder
                .Property(e => e.CreatedOn)
                .HasColumnName("created")
                .HasColumnType("datetime");
            
            builder
                .Property(e => e.ModifiedOn)
                .HasColumnName("modified")
                .HasColumnType("datetime");
        }
    }
}
