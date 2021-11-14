using System;
using Domain;

namespace Application.Queries.Works
{
    public class WorkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public string PhoneNumber { get; set; }
        public long? LowerPriceBound { get; set; }
        public long? UpperPriceBound { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
