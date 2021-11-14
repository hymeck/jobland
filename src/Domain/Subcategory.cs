using Domain.Common;

namespace Domain
{
    public class Subcategory : AuditableEntity
    {
        public string Name { get; set; }
        public Category ParentCategory { get; set; }
        public override string ToString() => Name;
    }
}
