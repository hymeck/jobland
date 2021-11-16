using Application.Common.Works;
using MediatR;

namespace Application.Queries.Works
{
    public sealed class GetWorkByNameQuery : IRequest<WorkResponse>
    {
        public GetWorkByNameQuery(string workName)
        {
            WorkName = workName;
        }

        public string WorkName { get; }
    }
}
