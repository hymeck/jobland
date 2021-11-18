namespace Jobland.Endpoints;

public static class WebApplicationExtensions
{
    public static WebApplication AddEndpoints(this WebApplication app) =>
        app
            .AddCategoryEndpoints()
            .AddAuthenticationEndpoints();
}
