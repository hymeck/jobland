namespace Jobland.Infrastructure.Common.Identity.Abstractions;

public interface IJwtTokenService
{
    public string GenerateJwtToken(User user);
}
