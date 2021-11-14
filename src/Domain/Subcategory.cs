using Domain.Common;

namespace Domain
{
    public class Subcategory : AuditableEntity
    {
        public string Name { get; set; }
        public long ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public override string ToString() => Name;
    }
}
