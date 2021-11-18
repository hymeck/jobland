namespace Jobland.Endpoints;

public static class HttpContextExtensions
{
    /// <summary>
    /// deserializes json from http request body to .net object.
    /// </summary>
    /// <param name="httpContext">http context.</param>
    /// <typeparam name="T">type of json object.</typeparam>
    /// <returns>if success, object of <see cref="T"/>, otherwise, <c>null</c>.</returns>
    public static async Task<T?> SafeGetJsonAsync<T>(this HttpContext? httpContext) where T : class
    {
        if (httpContext == null)
            return default;
        try
        {
            return await httpContext.Request.ReadFromJsonAsync<T>();
        }
        catch (Exception)
        {
            return default;
        }
    }
}
