using Jobland.Application.Logic.Works.Queries;
using Jobland.Domain.Core;
using LanguageExt;

namespace Jobland.Application.Logic.Abstractions;

public interface IWorkRepository
{
    public Task<Option<Work>> AddWorkAsync(Work work, CancellationToken token = default);
    public Task<Option<Work>> EditWorkAsync(Work work, CancellationToken token = default);
    public Task<Option<Work>> GetWorkByIdAsync(long workId, CancellationToken token = default);
    public Task<IEnumerable<Work>> GetWorksByTitleAsync(string title, CancellationToken token = default);
    public Task<IEnumerable<Work>> GetPaginatedWorksAsync(int offset, int limit, CancellationToken token = default);
    public Task<long> GetTotalWorksCountAsync(CancellationToken token = default);
    public Task<IEnumerable<Work>> GetWorksByFilterAsync(WorkSearchFilter filter, CancellationToken token = default);
    public Task<bool> RespondWorkAsync(long workId, CancellationToken token = default);
}
