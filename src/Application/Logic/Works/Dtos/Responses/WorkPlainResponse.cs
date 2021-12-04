using Jobland.Application.Logic.Categories.Dtos.Responses;
using Jobland.Application.Logic.Subcategories.Dtos.Responses;

namespace Jobland.Application.Logic.Works.Dtos.Responses;

public sealed record WorkPlainResponse(
    long Id,
    string Title,
    string Description,
    DateTime? StartedOn,
    DateTime? FinishedOn,
    string PhoneNumber,
    long? LowerPriceBound,
    long? UpperPriceBound,
    string AuthorId,
    long ResponseCount,
    CategoryPlainResponse Category,
    SubcategoryPlainResponse Subcategory,
    DateTime Added, DateTime Modified);
