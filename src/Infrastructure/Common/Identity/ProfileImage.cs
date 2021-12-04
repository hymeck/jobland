using Jobland.Domain.Common;

namespace Jobland.Infrastructure.Common.Identity;

public sealed class ProfileImage : AuditableEntity
{
    public string? OwnerId { get; set; }
    public User? Owner { get; set; }
    public string ImageUrl { get; set; } = "";
    public override string ToString() => ImageUrl;
}
