﻿using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Application.Logic.Works.Dtos.Responses;
using Jobland.Domain.Core;
using MediatR;

namespace Jobland.Application.Logic.Works.Queries;

public sealed class GetWorksByTitleRequestHandler 
    : IRequestHandler<GetWorksByTitleRequest, IEnumerable<WorkPlainResponse>>
{
    private readonly IMapper _mapper;
    private readonly IWorkRepository _repository;

    public GetWorksByTitleRequestHandler(IMapper mapper, IWorkRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<WorkPlainResponse>> Handle(GetWorksByTitleRequest request, CancellationToken cancellationToken)
    {
        var works = await _repository.GetWorksByTitleAsync(request.Title, cancellationToken);
        return _mapper.Map<IEnumerable<Work>, IEnumerable<WorkPlainResponse>>(works);
    }
}
