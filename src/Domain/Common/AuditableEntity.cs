namespace Jobland.Domain.Common;

public class AuditableEntity : Entity
{
    public AuditableEntity() => Added = Modified = DateTime.UtcNow;
    public DateTime? Added { get; set; }
    public DateTime? Modified { get; set; }
}
