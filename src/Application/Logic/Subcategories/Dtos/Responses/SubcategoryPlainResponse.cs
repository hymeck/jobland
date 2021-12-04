namespace Jobland.Application.Logic.Subcategories.Dtos.Responses;

/// <summary>
/// holds properties of <see cref="Jobland.Domain.Core.Subcategory"/> object
/// without associated <see cref="Jobland.Domain.Core.Category"/> object.
/// </summary>
public sealed record SubcategoryPlainResponse(long Id, string Name);
