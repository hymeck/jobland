using Jobland.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobland.Infrastructure.Common.Persistence.Configurations;

public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// sets primary key with name "id".
    /// </summary>
    /// <remarks>call it before all other field configurations to set primary key firstly.</remarks>
    public static EntityTypeBuilder<TEntity> SetDefaultId<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : Entity
    {
        builder
            .HasKey(e => e.Id)
            .HasName("id");
            
        return builder;
    }
    
    /// <summary>
    /// sets auditable fields with names "added" and "modified".
    /// </summary>
    /// <remarks>call it after all other field configurations to set auditable fields in the end of the table.</remarks>
    public static EntityTypeBuilder<TEntity> SetAuditableFields<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : AuditableEntity
    {
        builder
            .Property(e => e.Added)
            .IsRequired(true)
            .HasColumnName("added");
        
        builder
            .Property(e => e.Modified)
            .IsRequired(true)
            .HasColumnName("modified");

        return builder;
    }
}
