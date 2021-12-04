namespace Jobland.Application.Logic.Categories.Dtos.Responses;

/// <summary>
/// holds properties of <see cref="Jobland.Domain.Core.Category"/> object
/// without associated <see cref="Jobland.Domain.Core.Subcategory"/> object.
/// </summary>
public sealed record CategoryPlainResponse(long Id, string Name, string IconUrl);
