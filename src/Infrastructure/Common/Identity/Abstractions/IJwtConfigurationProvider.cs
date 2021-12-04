namespace Jobland.Infrastructure.Common.Identity.Abstractions;

public interface IJwtConfigurationProvider
{
    public JwtConfiguration GetJwtConfiguration();
}
