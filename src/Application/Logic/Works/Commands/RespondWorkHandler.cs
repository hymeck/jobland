using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Application.Logic.Works.Commands;

public sealed class RespondWorkRequestHandler : IRequestHandler<RespondWorkRequest, Option<RespondWorkResponse>>
{
    private readonly IWorkRepository _repository;

    public RespondWorkRequestHandler(IWorkRepository repository) => _repository = repository;

    public async Task<Option<RespondWorkResponse>> Handle(RespondWorkRequest request, CancellationToken cancellationToken)
    {
        var respondOption = await _repository.RespondWorkAsync(request.Id, request.ResponderId, cancellationToken);
        return respondOption.Map(success => new RespondWorkResponse(success));
    }
}
