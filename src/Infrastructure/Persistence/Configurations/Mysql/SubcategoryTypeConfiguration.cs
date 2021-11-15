using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Mysql
{
    public sealed class SubcategoryTypeConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder
                .ToTable("subcategory")
                .HasCharSet("utf8mb4");
            
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

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
