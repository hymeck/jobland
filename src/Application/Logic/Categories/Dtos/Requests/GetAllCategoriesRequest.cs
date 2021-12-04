using Jobland.Application.Logic.Categories.Dtos.Responses;
using MediatR;

namespace Jobland.Application.Logic.Categories.Dtos.Requests;

public sealed record GetAllCategoriesRequest : IRequest<IEnumerable<CategoryRichResponse>>;
