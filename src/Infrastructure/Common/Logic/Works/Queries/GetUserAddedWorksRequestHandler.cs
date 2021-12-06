using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Works.Dtos.Responses;
using Jobland.Domain.Core;
using Jobland.Infrastructure.Common.Logic.Works.Dtos.Requests;
using LanguageExt;
using MediatR;

namespace Jobland.Infrastructure.Common.Logic.Works.Queries;

public sealed class GetUserAddedWorksRequestHandler : IRequestHandler<GetUserAddedWorksRequest, Option<IEnumerable<WorkPlainResponse>>>
{
    private readonly IWorkRepository _repository;
    private readonly IMapper _mapper;

    public GetUserAddedWorksRequestHandler(IWorkRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Option<IEnumerable<WorkPlainResponse>>> Handle(GetUserAddedWorksRequest request, CancellationToken cancellationToken)
    {
        var resultOption = await _repository.GetWorkByAuthorIdAsync(request.UserId, cancellationToken);
        return resultOption.Map(works => _mapper.Map<IEnumerable<Work>, IEnumerable<WorkPlainResponse>>(works));
    }
}
