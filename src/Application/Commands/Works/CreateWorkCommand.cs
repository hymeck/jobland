using System;
using Application.Common.Works;
using MediatR;

namespace Application.Commands.Works
{
    public sealed class CreateWorkCommand : IRequest<WorkResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public string PhoneNumber { get; set; }
        public long? LowerPriceBound { get; set; }
        public long? UpperPriceBound { get; set; }
        public long CategoryId { get; set; }
        public long SubcategoryId { get; set; }
    }
}
