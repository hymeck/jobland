using Jobland.Application.Logic.Subcategories.Dtos.Responses;

namespace Jobland.Application.Logic.Categories.Dtos.Responses;

/// <summary>
/// contains <see cref="Jobland.Domain.Core.Category"/> properties with
/// associated <see cref="Jobland.Domain.Core.Subcategory"/> entities.
/// </summary>
public sealed record CategoryRichResponse(
    long Id,
    string Name,
    string IconUrl,
    List<SubcategoryPlainResponse> Subcategories);
