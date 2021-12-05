using Jobland.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Infrastructure.Common.Persistence;

public static class ApplicationDbContextExtensions
{
    public static IQueryable<Work> NoTrackingWorksWithIncludedEntities(this ApplicationDbContext db) =>
        db.Works
            .AsNoTracking()
            .Include(w => w.Subcategory)
            .ThenInclude(sc => sc!.Category);
    
    public static IQueryable<Category> NoTrackingCategoriesWithIncludedEntities(this ApplicationDbContext db) =>
        db.Categories
            .AsNoTracking()
            .Include(w => w.Subcategories);
}
