using System.Text;

namespace Jobland.Infrastructure.Common.Identity.Abstractions;

public class JwtConfiguration
{
    public string Key { get; init; } = "";
    public string Issuer { get; init; } = "";
    public TimeSpan ExpiresAfter { get; init; }
    // todo: add other fields (ValidateIssuer, ValidateAudience) to provide more flexible JWT auth middleware configuration
    
    public byte[] KeyBytes() => Encoding.ASCII.GetBytes(Key);
}
