using AutoMapper;
using Jobland.Dtos;
using Jobland.Models;
using Jobland.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Endpoints;

[JwtBearerAuthorize]
public static class CategoryEndpoints
{
    private const string CategoriesRoot = "/categories";
    private const string CategoriesId = $"{CategoriesRoot}/" + "{id}";

    public static WebApplication AddCategoryEndpoints(this WebApplication app) =>
        app
            .AddCategory()
            .GetCategory()
            .GetCategories()
            .UpdateCategory()
            .DeleteCategory();
    private static WebApplication AddCategory(this WebApplication app)
    {
        app.MapPost(CategoriesRoot, async (HttpContext http, ApplicationDbContext db, IMapper mapper) =>
        {
            var dto = await http.SafeGetJsonAsync<CategoryAddRequest>();
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
    
    private static WebApplication GetCategory(this WebApplication app)
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

    private static WebApplication GetCategories(this WebApplication app)
    {
        // var query = context.Request.Query;
        // string offsetStr = query["offset"];
        // string limitStr = query["limit"];
        // var offset = int.TryParse(offsetStr ?? "0", out var o) ? o : 0;
        // var limit = int.TryParse(limitStr ?? "50", out var l) ? l : 50;
        app.MapGet(CategoriesRoot, (ApplicationDbContext db, IMapper mapper) =>
        {
            var entities = db.Categories
                .AsNoTracking()
                .OrderByDescending(c => c.Added)
                .Include(c => c.Subcategories)
                .AsEnumerable();
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryListItemDto>>(entities);
        });
        return app;
    }

    private static WebApplication UpdateCategory(this WebApplication app)
    {
        app.MapPut(CategoriesId, async (HttpContext http, long id, ApplicationDbContext db) =>
        {
            var dto = await http.SafeGetJsonAsync<CategoryUpdateRequest>();
            if (dto == null || dto.Id != id)
                return Results.BadRequest();
            var entity = await db.Categories
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                return Results.UnprocessableEntity();

            db.Categories.Update(entity);
            if (dto.Name != null)
                entity.Name = dto.Name;
            if (dto.IconUrl != null)
                entity.IconUrl = dto.IconUrl;

            if (dto.Subcategories.Count != 0)
            {
                entity.Subcategories.Clear();
                // await db.SaveChangesAsync();
                foreach (var scId in dto.Subcategories)
                {
                    var subcategory = await db.Subcategories.FirstOrDefaultAsync(sc => sc.Id == scId);
                    if (subcategory != null)
                        entity.Subcategories.Add(subcategory);
                }

                db.Categories.Update(entity);
            }
            
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

    private static WebApplication DeleteCategory(this WebApplication app)
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
