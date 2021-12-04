using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Domain.Core;
using LanguageExt;
using MediatR;

namespace Jobland.Application.Logic.Works.Commands;

public sealed class UpdateWorkRequestHandler : IRequestHandler<UpdateWorkRequest, bool>
{
    private readonly IWorkRepository _repository;
    private readonly IMapper _mapper;

    public UpdateWorkRequestHandler(IWorkRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateWorkRequest request, CancellationToken cancellationToken)
    {
        var work = await _repository.GetWorkByIdAsync(request.Id, cancellationToken);
        var workOption = await work.MatchAsync(w =>
        {
            // todo: error-prone, rude and dirty imperative 2
            if (!string.IsNullOrEmpty(request.Description))
                w.Description = request.Description;
            if (!string.IsNullOrEmpty(request.Title))
                w.Title = request.Title;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                w.PhoneNumber = w.PhoneNumber;
            if (request.StartedOn.HasValue)
                w.StartedOn = request.StartedOn.GetValueOrDefault();
            if (request.FinishedOn.HasValue)
                w.FinishedOn = request.FinishedOn.GetValueOrDefault();
            if (request.LowerPriceBound.HasValue)
                w.LowerPriceBound = request.LowerPriceBound;
            if (request.UpperPriceBound.HasValue)
                w.UpperPriceBound = request.UpperPriceBound;
            return _repository.EditWorkAsync(w, cancellationToken);
        }, () => Option<Work>.None);

        return workOption.Match(_ => true, false);
    }
}
