using AutoMapper;
using Jobland.Dtos;
using Jobland.Models;
using Jobland.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Endpoints;

[JwtBearerAuthorize]
public static class WorkEndpoints
{
    public const string WorkRoot = "/works";
    public const string WorkIdRoot = WorkRoot + "/{id}";
    public const string WorkTitleRoot = WorkRoot + "-title";
    public const string WorkCount = WorkRoot + "/count";
    public const string WorkFilterRoot = WorkRoot + "/filter";

    public static WebApplication AddWorkEndpoints(this WebApplication app) =>
        app
            .AddWork()
            .GetWorkById()
            .GetWorksByTitle()
            .GetWorks()
            .GetWorkCount()
            .GetWorksByFilter();

    private static WebApplication GetWorkById(this WebApplication app)
    {
        app.MapGet(WorkIdRoot, async (long id, ApplicationDbContext db) =>
        {
            var entity = await db.NoTrackingWorksWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            return entity != null ? Results.Ok(entity) : Results.NotFound();
        });
        return app;
    }

    private static WebApplication GetWorksByTitle(this WebApplication app)
    {
        app.MapGet(WorkTitleRoot, (HttpContext http, ApplicationDbContext db, IMapper mapper) =>
        {
            var title = http.Request.Query["title"].ToString();
            if (string.IsNullOrEmpty(title))
                return Results.BadRequest();
            var entities = db.NoTrackingWorksWithIncludedEntities()
                .Where(w => w.Title.Contains(title))
                .AsEnumerable();

            return Results.Ok(mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities));
        });
        return app;
    }

    private static WebApplication AddWork(this WebApplication app)
    {
        app.MapPost(WorkRoot, async (HttpContext http, ApplicationDbContext db, IMapper mapper) =>
        {
            var dto = await http.SafeGetJsonAsync<WorkAddRequest>();
            if (dto == null)
                return Results.BadRequest();

            var subcategory = await db.Subcategories
                .Include(sc => sc.Category)
                .FirstOrDefaultAsync(sc => sc.Id == dto.SubcategoryId);
            if (subcategory == null)
                return Results.BadRequest();

            var entity = mapper.Map<WorkAddRequest, Work>(dto);
            entity.Subcategory = subcategory;
            entity.Category = subcategory.Category;
            entity.AuthorId = ""; // todo: extract userId from http when fix auth and put it here
            db.Works.Add(entity);
            var affected = await db.SaveChangesAsync();
            return affected > 0
                ? Results.Created(WorkIdRoot, mapper.Map<Work, WorkDto>(entity))
                : Results.UnprocessableEntity();
        });
        return app;
    }

    private static WebApplication GetWorks(this WebApplication app)
    {
        app.MapGet(WorkRoot, (HttpContext http, ApplicationDbContext db, IMapper mapper) =>
        {
            var query = http.Request.Query;
            var offsetStr = (string)query["offset"] ?? "0";
            var limitStr = (string)query["limit"] ?? "50";
            if (!int.TryParse(offsetStr, out var o) || !int.TryParse(limitStr, out var l))
                return Results.BadRequest();
            if (o < 0 || l < 0)
                return Results.BadRequest();
            var entities = db.NoTrackingWorksWithIncludedEntities()
                .DescendingOrderedByAdded()
                .Skip(o).Take(l).AsEnumerable();
            return Results.Ok(mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities));
        });
        return app;
    }

    private static WebApplication GetWorkCount(this WebApplication app)
    {
        app.MapGet(WorkCount, async (ApplicationDbContext db) => Results.Ok(new WorkCountDto(await db.Works.CountAsync())));
        return app;
    }

    private static WebApplication GetWorksByFilter(this WebApplication app)
    {
        app.MapGet(WorkFilterRoot,  (HttpContext http, ApplicationDbContext db, IMapper mapper,
            [FromQuery] long? lowerPriceBound,
            [FromQuery] long? upperPriceBound, 
            [FromQuery] DateTime? started,
            [FromQuery] DateTime? finished,
            [FromQuery] bool? withResponses) =>
        {
            var ids = http.Request.Query["subcategories"].ToString()?.Split(',')
                .Select(id => long.TryParse(id, out var value) ? value : 0)
                .Where(id => id > 0)
                .ToList();
            var lower = lowerPriceBound;
            var upper = upperPriceBound;
            var startedOn = started;
            var finishedOn = finished;
            var responded = withResponses; // todo: add it to business logic

            var entities = db.NoTrackingWorksWithIncludedEntities();
            
            if (ids != null && ids.Count != 0)
                entities = entities.Where(w => ids.Contains(w.SubcategoryId));
            if (lower != null)
                entities = entities.Where(w => w.LowerPriceBound >= lower.GetValueOrDefault());
            if (upper != null) 
                entities = entities.Where(w => w.LowerPriceBound <= upper.GetValueOrDefault());
            if (startedOn != null) 
                entities = entities.Where(w => w.StartedOn >= started.GetValueOrDefault());
            if (finishedOn != null) 
                entities = entities.Where(w => w.FinishedOn <= finished.GetValueOrDefault());
            if (responded != null && !responded.GetValueOrDefault())
                entities = entities.Where(w => w.ResponseCount == 0);

#if DEBUG
            var dtos = mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities.AsEnumerable()).ToArray();
            return Results.Ok(dtos);
#else
            return Results.Ok(mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities.AsEnumerable()));
#endif
        });
        return app;
    }

    private static IQueryable<Work> NoTrackingWorksWithIncludedEntities(this ApplicationDbContext db) =>
        db.Works
            .AsNoTracking()
            .Include(w => w.Category)
            .Include(w => w.Subcategory);

    private static IOrderedQueryable<Work> DescendingOrderedByAdded(this IQueryable<Work> works) => works.OrderByDescending(w => w.Added);
}
