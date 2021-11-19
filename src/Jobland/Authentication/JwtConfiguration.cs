using System.Globalization;
using System.Text;

namespace Jobland.Authentication;

public class JwtConfiguration
{
    public string Key { get; init; } = "";
    public string Issuer { get; init; } = "";
    public TimeSpan ExpiresAfter { get; init; }
    // todo: add other fields (ValidateIssuer, ValidateAudience) to provide more flexible JWT auth middleware configuration
}

public static class JwtConfigurationExtensions
{
    public static byte[] KeyBytes(this JwtConfiguration jwt) => Encoding.ASCII.GetBytes(jwt.Key);
}

public interface IJwtConfigurationProvider
{
    public JwtConfiguration GetJwtConfiguration();
}

public class JwtConfigurationProvider : IJwtConfigurationProvider
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    public JwtConfigurationProvider(IConfiguration configuration, IWebHostEnvironment env)
    {
        _config = configuration;
        _env = env;
    }

    public JwtConfiguration GetJwtConfiguration() => new()
    {
        Key = ExtractKey(),
        Issuer = ExtractIssuer(),
        ExpiresAfter = ExtractExpiresAfter()
    };

    private string ExtractKey() => _config.JwtKey(_env);
    private string ExtractIssuer() => _config.JwtIssuer();
    private TimeSpan ExtractExpiresAfter()
    {
        var defaultExpires = TimeSpan.FromDays(7);
        try
        {
            return TimeSpan.TryParse(_config.JwtExpires(), CultureInfo.InvariantCulture, out var expires)
                ? expires
                : defaultExpires;
        }
        catch (Exception)
        {
            return defaultExpires;
        }
    }
}
