using AutoMapper;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Logic.Categories.Dtos.Requests;
using Jobland.Application.Logic.Categories.Dtos.Responses;
using Jobland.Domain.Core;
using MediatR;

namespace Jobland.Application.Logic.Categories.Queries;

public sealed class GetAllCategoriesRequestHandler : IRequestHandler<GetAllCategoriesRequest, IEnumerable<CategoryRichResponse>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCategoriesRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryRichResponse>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllCategories(cancellationToken);
        return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryRichResponse>>(categories);
    }
}
