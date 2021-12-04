using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using Jobland.Domain.Core;
using LanguageExt;
using MediatR;

namespace Jobland.Application.Logic.Works.Queries;


public sealed class GetWorkByIdRequestHandler : IRequestHandler<GetWorkByIdRequest, Option<WorkPlainResponse>>
{
    private readonly IMapper _mapper;
    private readonly IWorkRepository _repository;

    public GetWorkByIdRequestHandler(IMapper mapper, IWorkRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Option<WorkPlainResponse>> Handle(GetWorkByIdRequest request, CancellationToken cancellationToken)
    {
        var workOption = await _repository.GetWorkByIdAsync(request.Id, cancellationToken);
        return workOption.Map(w => _mapper.Map<Work, WorkPlainResponse>(w));
    }
}
