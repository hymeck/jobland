using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jobland.Infrastructure.Common.Identity;
using Jobland.Infrastructure.Common.Identity.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace Jobland.Infrastructure.Api.Web.Identity;


public class JwtTokenService : IJwtTokenService
{
    private readonly JwtConfiguration _jwt;
    public JwtTokenService(IJwtConfigurationProvider provider) => _jwt = provider.GetJwtConfiguration();

    public string GenerateJwtToken(User user)
    {
        var key = _jwt.KeyBytes();
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var jwtHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtHandler.CreateToken(descriptor);
        return jwtHandler.WriteToken(securityToken);
    }
}
