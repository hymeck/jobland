using Jobland.Domain.Common;

namespace Jobland.Domain.Core;

/// <summary>
/// represents category of work.
/// </summary>
public sealed class Category : AuditableEntity
{
    public string Name { get; set; } = "";
    public string IconUrl { get; set; } = "";
    public HashSet<Subcategory> Subcategories { get; } = new();
    public override string ToString() => Name;
}
