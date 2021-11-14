using MediatR;

namespace Application.Queries.Works
{
    public sealed class GetWorkByNameQuery : IRequest<GetWorkByNameQueryResponse>
    {
        public GetWorkByNameQuery(string workName)
        {
            WorkName = workName;
        }

        public string WorkName { get; }
    }
}
