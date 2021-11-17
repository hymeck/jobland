using Jobland.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("RemoteMysql"); 
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Subcategory> Subcategories => Set<Subcategory>();
    public DbSet<Work> Works => Set<Work>();

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
                    entry.Entity.Modified = now;
                    break;
                case EntityState.Added:
                    entry.Entity.Added = now;
                    entry.Entity.Modified = now;
                    break;
            }
        }
    }
}
