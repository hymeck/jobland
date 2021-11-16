using System;
using Application.Common.Categories;
using Application.Common.Subcategories;

namespace Application.Common.Works
{
    public class WorkDto
    {
        public long WorkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public string PhoneNumber { get; set; }
        public long? LowerPriceBound { get; set; }
        public long? UpperPriceBound { get; set; }
        public CategoryDto Category { get; set; }
        public SubcategoryDto Subcategory { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
