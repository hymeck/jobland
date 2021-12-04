using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using MediatR;

namespace Jobland.Application.Logic.Works.Commands;

public sealed class RespondWorkRequestHandler : IRequestHandler<RespondWorkRequest, bool>
{
    private readonly IWorkRepository _repository;

    public RespondWorkRequestHandler(IWorkRepository repository) => _repository = repository;

    public async Task<bool> Handle(RespondWorkRequest request, CancellationToken cancellationToken) => 
        await _repository.RespondWorkAsync(request.Id, cancellationToken);
}
