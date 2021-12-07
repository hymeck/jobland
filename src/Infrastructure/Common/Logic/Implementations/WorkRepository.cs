using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Extensions;
using Jobland.Application.Logic.Works.Queries;
using Jobland.Domain.Core;
using Jobland.Infrastructure.Common.Persistence;
using LanguageExt;
using LanguageExt.SomeHelp;
using LanguageExt.UnsafeValueAccess;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Infrastructure.Common.Logic.Implementations;

public class WorkRepository : IWorkRepository
{
    private readonly ApplicationDbContext _db;

    public WorkRepository(ApplicationDbContext db) => _db = db;

    public async Task<Option<Work>> AddWorkAsync(Work work, CancellationToken token = default)
    {
        var subcategory = await _db.Subcategories
            .Include(sc => sc.Category)
            .FirstOrDefaultAsync(sc => sc.Id == work.SubcategoryId, token);
        if (subcategory == null)
            return Option<Work>.None;
        work.Subcategory = subcategory;
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
        var works = _db
            .NoTrackingWorksWithIncludedEntities()
            .OrderedByDescendingAdded()
            .ApplyPagination(offset, limit);
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
        if (!string.IsNullOrEmpty(filter.Title))
            works = works.Where(w => w.Title.Contains(filter.Title));
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

    public async Task<Option<bool>> RespondWorkAsync(long workId, string responderId, CancellationToken token = default)
    {
        if (!await _db.Users.AnyAsync(u => u.Id == responderId, token))
            return Option<bool>.None;
        if (await _db.WorkResponses.AnyAsync(wr => wr.ResponderId == responderId && wr.WorkId == workId, token))
            return false;
        
        var workOption = await GetWorkByIdAsync(workId, token);
        var success = await workOption.MatchAsync(w =>
        {
            // todo: referential transparency is violated
            _db.Works.Update(w.IncrementResponses());
            return _db.SaveChangesAsync(token);
        }, () => -1) > 0;
        
        if (success)
        {
            var response = new WorkResponse { Work = workOption.ValueUnsafe(), ResponderId = responderId };
            _db.WorkResponses.Add(response);
            return (await _db.SaveChangesSafelyAsync(token)).Map(a => a > 0);
        }
        
        return success;
    }

    public async Task<Option<IEnumerable<Work>>> GetWorkByAuthorIdAsync(string authorId, CancellationToken token = default) =>
        await _db.Users.AnyAsync(u => u.Id == authorId, token)
            ? _db
                .NoTrackingWorksWithIncludedEntities()
                .Where(w => w.AuthorId == authorId)
                .OrderedByDescendingAdded()
                .AsEnumerable().ToSome()
            : Option<IEnumerable<Work>>.None;
}
