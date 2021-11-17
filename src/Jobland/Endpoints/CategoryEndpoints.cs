using AutoMapper;
using Jobland.Dtos;
using Jobland.Models;
using Jobland.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Endpoints;

public static class CategoryEndpoints
{
    private const string CategoriesRoot = "/categories";
    private const string CategoriesId = $"{CategoriesRoot}/" + "{id}";
    internal static WebApplication AddCategory(this WebApplication app)
    {
        app.MapPost(CategoriesRoot, async (HttpContext http, ApplicationDbContext db, IMapper mapper) =>
        {
            var dto = await http.Request.ReadFromJsonAsync<CategoryAddRequest>();
            if (dto == null)
                return Results.BadRequest();

            var entity = mapper.Map<CategoryAddRequest, Category>(dto);

            await db.Categories.AddAsync(entity);
            var affected = await db.SaveChangesAsync();
            return affected > 0
                ? Results.Created(CategoriesRoot, entity)
                : Results.UnprocessableEntity();
        });
        return app;
    }
    
    internal static WebApplication GetCategory(this WebApplication app)
    {
        app.MapGet(CategoriesId, async (long id, ApplicationDbContext db) =>
        {
            var entity = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return entity != null
                ? Results.Ok(entity)
                : Results.NotFound();
        });
        return app;
    }

    internal static WebApplication GetCategories(this WebApplication app)
    {
        app.MapGet(CategoriesRoot, (ApplicationDbContext db) => db.Categories.AsNoTracking().AsEnumerable());
        return app;
    }

    internal static WebApplication UpdateCategory(this WebApplication app)
    {
        app.MapPut(CategoriesId, async (HttpContext http, long id, ApplicationDbContext db) =>
        {
            var dto = await http.Request.ReadFromJsonAsync<CategoryUpdateRequest>();
            if (dto == null || dto.Id != id)
                return Results.BadRequest();
            var entity = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                return Results.UnprocessableEntity();

            db.Categories.Update(entity);
            entity.Name = dto.Name;
            entity.IconUrl = dto.IconUrl;
            try
            {
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception)
            {
                return Results.UnprocessableEntity();
            }
        });
        return app;
    }

    internal static WebApplication DeleteCategory(this WebApplication app)
    {
        app.MapDelete(CategoriesId, async (long id, ApplicationDbContext db) =>
        {
            var entity = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                return Results.NotFound();
            
            db.Categories.Remove(entity);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        return app;
    }
}
