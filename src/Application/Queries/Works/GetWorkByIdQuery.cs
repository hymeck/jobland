using Application.Common.Works;
using MediatR;

namespace Application.Queries.Works
{
    public sealed class GetWorkByIdQuery : IRequest<WorkResponse>
    {
        public GetWorkByIdQuery(long workId)
        {
            WorkId = workId;
        }

        public long WorkId { get; }
    }
}
