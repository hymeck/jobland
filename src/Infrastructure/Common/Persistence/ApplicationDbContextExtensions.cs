using Jobland.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Infrastructure.Common.Persistence;

public static class ApplicationDbContextExtensions
{
    public static IQueryable<Work> NoTrackingWorksWithIncludedEntities(this ApplicationDbContext db) =>
        db.Works
            .AsNoTracking()
            .Include(w => w.Category)
            .Include(w => w.Subcategory);
    
    public static IQueryable<Category> NoTrackingCategoriesWithIncludedEntities(this ApplicationDbContext db) =>
        db.Categories
            .AsNoTracking()
            .Include(w => w.Subcategories);
}
