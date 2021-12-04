using System.Globalization;
using Jobland.Infrastructure.Common.Identity.Abstractions;

namespace Jobland.Infrastructure.Api.Web.Identity;

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
