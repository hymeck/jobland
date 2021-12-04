using Jobland.Domain.Common;

namespace Jobland.Domain.Core;

/// <summary>
/// represents subcategory of work.
/// </summary>
public sealed class Subcategory : AuditableEntity
{
    public string Name { get; set; } = "";
    public long CategoryId { get; set; }
    public Category? Category { get; set; }
    public override string ToString() => Name;
}
