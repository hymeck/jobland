using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using Jobland.Application.Logic.Works.Extensions;
using Jobland.Domain.Core;
using MediatR;

namespace Jobland.Application.Logic.Works.Queries;

public sealed class GetWorksByFilterRequestHandler : IRequestHandler<GetWorksByFilterRequest, IEnumerable<WorkPlainResponse>>
{
    private readonly IMapper _mapper;
    private readonly IWorkRepository _repository;

    public GetWorksByFilterRequestHandler(IMapper mapper, IWorkRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<WorkPlainResponse>> Handle(GetWorksByFilterRequest request, CancellationToken cancellationToken)
    {
        var filter = new WorkSearchFilter
        {
            Finished = request.Finished,
            LowerPriceBound = request.LowerPriceBound,
            Started = request.Started,
            UpperPriceBound = request.UpperPriceBound,
            WithResponses = request.WithResponses,
            Subcategories = request.Subcategories
        };
        var works = await _repository.GetWorksByFilterAsync(filter, cancellationToken);
        return _mapper.Map<IEnumerable<Work>, IEnumerable<WorkPlainResponse>>(works.ApplyPagination(request.Offset, request.Limit));
    }
}
