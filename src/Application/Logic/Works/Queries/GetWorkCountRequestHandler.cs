using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using MediatR;

namespace Jobland.Application.Logic.Works.Queries;


public sealed class GetWorkCountRequestHandler : IRequestHandler<GetWorkCountRequest, WorkCountResponse>
{
    private readonly IWorkRepository _repository;

    public GetWorkCountRequestHandler(IWorkRepository repository) => _repository = repository;

    public async Task<WorkCountResponse> Handle(GetWorkCountRequest request, CancellationToken cancellationToken) =>
        new(await _repository.GetTotalWorksCountAsync(cancellationToken));
}
