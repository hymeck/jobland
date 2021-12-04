using LanguageExt;

namespace Jobland.Application.Persistence.Abstractions;

public interface IApplicationDbContext
{
    public Task<Option<int>> SaveChangesSafelyAsync(CancellationToken cancellation = default);
}
