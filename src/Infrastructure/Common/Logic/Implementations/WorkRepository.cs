using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Queries;
using Jobland.Domain.Core;
using Jobland.Infrastructure.Common.Persistence;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Infrastructure.Common.Logic.Implementations;

public class WorkRepository : IWorkRepository
{
    private readonly ApplicationDbContext _db;

    public WorkRepository(ApplicationDbContext db) => _db = db;

    public async Task<Option<Work>> AddWorkAsync(Work work, CancellationToken token = default)
    {
        _db.Works.Add(work);
        var option = await _db.SaveChangesSafelyAsync(token);
        return option.Match(_ => work, Option<Work>.None);
    }

    public async Task<Option<Work>> EditWorkAsync(Work work, CancellationToken token = default)
    {
        _db.Works.Update(work);
        var affectedOption = await _db.SaveChangesSafelyAsync(token);
        return affectedOption.Match(_ => work, Option<Work>.None);
    }

    public async Task<Option<Work>> GetWorkByIdAsync(long workId, CancellationToken token = default)
    {
        var work = await _db
            .NoTrackingWorksWithIncludedEntities()
            .FirstOrDefaultAsync(w => w.Id == workId, token);
        return work ?? Option<Work>.None;
    }

    public Task<IEnumerable<Work>> GetWorksByTitleAsync(string title, CancellationToken token = default) =>
        Task.FromResult(_db
            .NoTrackingWorksWithIncludedEntities()
            .Where(w => w.Title.Contains(title))
            .OrderedByDescendingAdded()
            .AsEnumerable());

    public Task<IEnumerable<Work>> GetPaginatedWorksAsync(int offset, int limit, CancellationToken token = default)
    {
        var works = (offset, limit) switch
        {
            (< 0, _) => Enumerable.Empty<Work>(),
            (_, <= 0) => Enumerable.Empty<Work>(),
            _ => _db.NoTrackingWorksWithIncludedEntities()
                .OrderedByDescendingAdded()
                .Skip(offset).Take(limit)
        };
        return Task.FromResult(works);
    }

    public async Task<long> GetTotalWorksCountAsync(CancellationToken token = default) => await _db.Works.LongCountAsync(token);

    public Task<IEnumerable<Work>> GetWorksByFilterAsync(WorkSearchFilter filter, CancellationToken token = default)
    {
        var ids = filter.Subcategories;
        var lower = filter.LowerPriceBound;
        var upper = filter.UpperPriceBound;
        var startedOn = filter.Started;
        var finishedOn = filter.Finished;
        var responded = filter.WithResponses;

        var works = _db.NoTrackingWorksWithIncludedEntities();

        // todo: error-prone, rude and dirty imperative
        if (ids != null && ids.Count != 0)
            works = works.Where(w => ids.Contains(w.SubcategoryId));
        if (lower != null)
            works = works.Where(w => w.LowerPriceBound >= lower.GetValueOrDefault());
        if (upper != null) 
            works = works.Where(w => w.LowerPriceBound <= upper.GetValueOrDefault());
        if (startedOn != null) 
            works = works.Where(w => w.StartedOn >= startedOn.GetValueOrDefault());
        if (finishedOn != null) 
            works = works.Where(w => w.FinishedOn <= finishedOn.GetValueOrDefault());
        if (responded != null && !responded.GetValueOrDefault())
            works = works.Where(w => w.ResponseCount == 0);

        return Task.FromResult(works.AsEnumerable());
    }

    public async Task<bool> RespondWorkAsync(long workId, CancellationToken token = default)
    {
        var workOption = await GetWorkByIdAsync(workId, token);
        var affected = await workOption.MatchAsync(w =>
        {
            // todo: referential transparency is violated
            _db.Works.Update(w);
            return _db.SaveChangesAsync(token);
        }, () => -1);
        return affected > 0;
    }
}
