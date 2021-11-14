using System;
using Domain.Common;

namespace Domain
{
    public class Work : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public string PhoneNumber { get; set; }
        public long? LowerPriceBound { get; set; }
        public long? UpperPriceBound { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public long SubcategoryId { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public override string ToString() => Name;
    }
}
