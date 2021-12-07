using Jobland.Domain.Common;

namespace Jobland.Domain.Core;

public class WorkResponse : AuditableEntity
{
    public long WorkId { get; set; }
    public Work? Work { get; set; }
    public string ResponderId { get; set; } = "";
}
