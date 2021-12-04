using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using Jobland.Domain.Core;
using MediatR;

namespace Jobland.Application.Logic.Works.Queries;

public sealed class GetWorksRequestHandler : IRequestHandler<GetWorksRequest, IEnumerable<WorkPlainResponse>>
{
    private readonly IMapper _mapper;
    private readonly IWorkRepository _repository;

    public GetWorksRequestHandler(IMapper mapper, IWorkRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<WorkPlainResponse>> Handle(GetWorksRequest request, CancellationToken cancellationToken)
    {
        var (offset, limit) = request;
        var works = await _repository.GetPaginatedWorksAsync(offset, limit, cancellationToken);
        return _mapper.Map<IEnumerable<Work>, IEnumerable<WorkPlainResponse>>(works);
    }
}
