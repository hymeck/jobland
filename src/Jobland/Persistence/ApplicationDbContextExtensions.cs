using Jobland.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Persistence;

public static class ApplicationDbContextExtensions
{
    public static IQueryable<Work> NoTrackingWorksWithIncludedEntities(this ApplicationDbContext db) =>
        db.Works
            .AsNoTracking()
            .Include(w => w.Category)
            .Include(w => w.Subcategory);

    public static IOrderedQueryable<Work> DescendingOrderedByAdded(this IQueryable<Work> works) => works.OrderByDescending(w => w.Added);
}
