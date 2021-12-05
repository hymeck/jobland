using System.Reflection;
using Jobland.Application.Persistence.Abstractions;
using Jobland.Domain.Common;
using Jobland.Domain.Core;
using Jobland.Infrastructure.Common.Identity;
using Jobland.Infrastructure.Common.Messenger;
using LanguageExt;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jobland.Infrastructure.Common.Persistence;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("RemoteMysql"); 
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // apply implementations of IEntityTypeConfiguration
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Subcategory> Subcategories => Set<Subcategory>();

    public DbSet<Work> Works => Set<Work>();
    public DbSet<ProfileImage> ProfileImages => Set<ProfileImage>();
    public DbSet<DirectMessage> DirectMessages => Set<DirectMessage>();

    public async Task<Option<int>> SaveChangesSafelyAsync(CancellationToken cancellation = default)
    {
        var saveResult = await TrySaveChangesAsync(cancellation)();
        return saveResult.Match(affected => affected, _ => Option<int>.None);
    }

    public override Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        SetDates();
        return base.SaveChangesAsync(token);
    }

    protected TryAsync<int> TrySaveChangesAsync(CancellationToken cancellation) =>
        async () => await SaveChangesAsync(cancellation);

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
