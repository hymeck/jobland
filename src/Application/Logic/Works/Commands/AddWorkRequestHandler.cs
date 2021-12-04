using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using Jobland.Domain.Core;
using LanguageExt;
using MediatR;

namespace Jobland.Application.Logic.Works.Commands;


public sealed class AddWorkRequestHandler : IRequestHandler<AddWorkRequest, Option<WorkPlainResponse>>
{
    private readonly IWorkRepository _repository;
    private readonly IMapper _mapper;

    public AddWorkRequestHandler(IWorkRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Option<WorkPlainResponse>> Handle(AddWorkRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<AddWorkRequest, Work>(request);
        var result = await _repository.AddWorkAsync(entity, cancellationToken);
        return result.Map(w => _mapper.Map<Work, WorkPlainResponse>(w));
    }
}
