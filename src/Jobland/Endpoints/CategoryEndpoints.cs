using AutoMapper;
using Jobland.Dtos;
using Jobland.Models;
using Jobland.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Endpoints;

public class CategoryEndpoints : ApiEndpointBase
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CategoryEndpoints(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("/categories")]
    public IActionResult GetCategories()
    {
        var entities = _db.Categories
            .AsNoTracking()
            .OrderByDescending(c => c.Added)
            .Include(c => c.Subcategories)
            .AsEnumerable();
        return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryListItemDto>>(entities));
    }
}
