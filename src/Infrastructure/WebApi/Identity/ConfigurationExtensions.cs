namespace Jobland.Infrastructure.Api.Web.Identity;

public static class ConfigurationExtensions
{
    public static string JwtKey(this IConfiguration config, IWebHostEnvironment env) =>
        env.IsDevelopment() ? config["Jwt:Key"] : Environment.GetEnvironmentVariable("JwtKey") ?? "";

    public static string JwtIssuer(this IConfiguration config) => config["Jwt:Issuer"];
    public static string JwtExpires(this IConfiguration config) => config["Jwt:Expires"];
}
