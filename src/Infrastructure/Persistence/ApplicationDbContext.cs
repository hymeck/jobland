using System.Reflection;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<Work> Works => Set<Work>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Subcategory> Subcategories => Set<Subcategory>();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SetupRelationship(builder);
            base.OnModelCreating(builder);
        }

        private static void SetupRelationship(ModelBuilder builder)
        {
            builder
                .Entity<Subcategory>()
                .HasOne<Category>(sc => sc.ParentCategory)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(sc => sc.ParentCategoryId);
        }
    }
}
