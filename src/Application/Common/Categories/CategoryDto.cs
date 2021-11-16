using System.Collections.Generic;
using Application.Common.Subcategories;

namespace Application.Common.Categories
{
    public class CategoryDto
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string? IconUrl { get; set; }
        public List<SubcategoryDto> Subcategories { get; set; } = new();
        public override string ToString() => Name;
    }
}
