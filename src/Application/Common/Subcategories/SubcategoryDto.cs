using Application.Common.Categories;

namespace Application.Common.Subcategories
{
    public class SubcategoryDto
    {
        public long SubcategoryId { get; set; }
        public string Name { get; set; }
        public virtual CategoryDto Category { get; set; }
        public override string ToString() => Name;
    }
}
