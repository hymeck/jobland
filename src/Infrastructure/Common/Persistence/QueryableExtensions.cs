using Jobland.Domain.Common;

namespace Jobland.Infrastructure.Common.Persistence;

public static class QueryableExtensions
{
    public static IOrderedQueryable<TEntity> OrderedByDescendingAdded<TEntity>(this IQueryable<TEntity> auditableEntities) 
        where TEntity : AuditableEntity =>
        auditableEntities.OrderByDescending(w => w.Added);
}
