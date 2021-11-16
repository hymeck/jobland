using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Common;
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

        public override Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            SetDates();
            return base.SaveChangesAsync(token);
        }

        private void SetDates()
        {
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                var now = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = now;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedOn = now;
                        entry.Entity.ModifiedOn = now;
                        break;
                }
            }
        }
    }
}
