using Jobland.Domain.Core;

namespace Jobland.Application.Logic.Abstractions;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAllCategories(CancellationToken token = default);
}
