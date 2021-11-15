using System.Collections.Generic;
using Domain.Common;

namespace Domain
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public string? IconUrl { get; set; }
        public List<Subcategory> Subcategories { get; set; } = new();
        public override string ToString() => Name;
    }
}
