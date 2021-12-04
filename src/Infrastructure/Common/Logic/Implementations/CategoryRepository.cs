using Jobland.Application.Logic.Abstractions;
using Jobland.Domain.Core;
using Jobland.Infrastructure.Common.Persistence;

namespace Jobland.Infrastructure.Common.Logic.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db) => _db = db;

    public Task<IEnumerable<Category>> GetAllCategories(CancellationToken token = default)
    {
        var categories = _db
            .NoTrackingCategoriesWithIncludedEntities()
            .OrderedByDescendingAdded()
            .AsEnumerable();
        return Task.FromResult(categories);
    }
}
