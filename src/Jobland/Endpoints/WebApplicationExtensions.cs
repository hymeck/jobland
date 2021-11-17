namespace Jobland.Endpoints;

public static class WebApplicationExtensions
{
    public static WebApplication AddEndpoints(this WebApplication app) =>
        app
            .AddCategory()
            .GetCategory()
            .GetCategories()
            .UpdateCategory()
            .DeleteCategory();
}
