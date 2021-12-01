using AutoMapper;
using Jobland.Dtos;
using Jobland.Models;
using Jobland.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Endpoints;

public class WorkEndpoints : ApiEndpointBase
{
    public const string WorkRoot = "/works";
    public const string WorkIdRoot = WorkRoot + "/{id}";
    public const string WorkTitleRoot = WorkRoot + "-title";
    public const string WorkCountRoot = WorkRoot + "/count";
    public const string WorkFilterRoot = WorkRoot + "/filter";
    public const string RespondWorkRoot = WorkRoot + "/respond";

    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public WorkEndpoints(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpPost(WorkRoot)]
    public async Task<IActionResult> AddWork([FromBody] WorkAddRequest? dto)
    {
        if (dto == null)
            return BadRequest();

        var subcategory = await _db.Subcategories
            .Include(sc => sc.Category)
            .FirstOrDefaultAsync(sc => sc.Id == dto.SubcategoryId);
        if (subcategory == null)
            return BadRequest();

        var entity = _mapper.Map<WorkAddRequest, Work>(dto);
        entity.Subcategory = subcategory;
        entity.Category = subcategory.Category;
        entity.AuthorId = CurrentUserId;
        _db.Works.Add(entity);
        var affected = await _db.SaveChangesAsync();
        return affected > 0
            ? Created(WorkIdRoot, _mapper.Map<Work, WorkDto>(entity))
            : UnprocessableEntity();
    }

    [HttpGet(WorkIdRoot)]
    public async Task<IActionResult> GetWorkById(long id)
    {
        var entity = await _db.NoTrackingWorksWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
        return entity != null ? Ok(entity) : NotFound();
    }

    [HttpGet(WorkTitleRoot)]
    public IActionResult GetWorksByTitle([FromQuery] string? title)
    {
        if (string.IsNullOrEmpty(title))
            return BadRequest();
        var entities = _db.NoTrackingWorksWithIncludedEntities()
            .DescendingOrderedByAdded()
            .Where(w => w.Title.Contains(title))
            .AsEnumerable();

        return Ok(_mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities));
    }
    
    [HttpGet(WorkRoot)]
    public IActionResult GetWorks([FromQuery] string? offset, [FromQuery] string? limit)
    {
        var offsetStr = offset ?? "0";
        var limitStr = limit ?? "50";
        if (!int.TryParse(offsetStr, out var o) || !int.TryParse(limitStr, out var l))
            return BadRequest();
        if (o < 0 || l < 0)
            return BadRequest();
        var entities = _db.NoTrackingWorksWithIncludedEntities()
            .DescendingOrderedByAdded()
            .Skip(o).Take(l).AsEnumerable();
        return Ok(_mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities));
    }
    
    [HttpGet(WorkCountRoot)]
    public async Task<IActionResult> GetWorkCount() => Ok(new WorkCountDto(await _db.Works.CountAsync()));

    [HttpGet(WorkFilterRoot)]
    public IActionResult GetWorksByFilter(
        [FromQuery] long? lowerPriceBound,
        [FromQuery] long? upperPriceBound, 
        [FromQuery] DateTime? started,
        [FromQuery] DateTime? finished,
        [FromQuery] bool? withResponses)
    {
        var ids = HttpContext.Request.Query["subcategories"].ToString()?.Split(',')
            .Select(id => long.TryParse(id, out var value) ? value : 0)
            .Where(id => id > 0)
            .Distinct()
            .ToList();
        var lower = lowerPriceBound;
        var upper = upperPriceBound;
        var startedOn = started;
        var finishedOn = finished;
        var responded = withResponses; // todo: add it to business logic

        var entities = _db.NoTrackingWorksWithIncludedEntities();
            
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
        var dtos = _mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities.AsEnumerable()).ToArray();
        return Ok(dtos);
#else
            return Ok(_mapper.Map<IEnumerable<Work>, IEnumerable<WorkDto>>(entities.AsEnumerable()));
#endif
    }

    [HttpPost(RespondWorkRoot)]
    public async Task<IActionResult> RespondWork([FromQuery] long id)
    {
        var work = await _db.Works.FirstOrDefaultAsync(w => w.Id == id);
        if (work == null)
            return BadRequest();
        _db.Works.Update(work.IncrementResponses());
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut(WorkRoot)]
    public async Task<IActionResult> EditWork([FromBody] WorkEditRequest? dto)
    {
        if (dto == null)
            return BadRequest();

        var entity = await _db.Works.FirstOrDefaultAsync(w => w.Id == dto.Id);
        if (entity == null)
            return NotFound();

        if (entity.AuthorId != CurrentUserId)
            return Forbid();

        if (!string.IsNullOrEmpty(dto.Description))
            entity.Description = dto.Description;
        if (!string.IsNullOrEmpty(dto.Title))
            entity.Title = dto.Title;
        if (!string.IsNullOrEmpty(dto.PhoneNumber))
            entity.PhoneNumber = entity.PhoneNumber;
        if (dto.StartedOn.HasValue)
            entity.StartedOn = dto.StartedOn.GetValueOrDefault();
        if (dto.FinishedOn.HasValue)
            entity.FinishedOn = dto.FinishedOn.GetValueOrDefault();
        if (dto.LowerPriceBound.HasValue)
            entity.LowerPriceBound = dto.LowerPriceBound;
        if (dto.UpperPriceBound.HasValue)
            entity.UpperPriceBound = dto.UpperPriceBound;

        if (dto.SubcategoryId.HasValue)
        {
            var subcategory = await _db.Subcategories.FirstOrDefaultAsync(sc => sc.Id == dto.SubcategoryId.GetValueOrDefault());
            if (subcategory != null)
                entity.Subcategory = subcategory;
        }
            
        _db.Works.Add(entity);
        await _db.SaveChangesAsync();
        
        return NoContent();
    }
}
