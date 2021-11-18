namespace Jobland.Dtos;

public sealed record CategoryAddRequest(string Name, string? IconUrl);
public sealed record CategoryUpdateRequest(long Id, string? Name, string? IconUrl, HashSet<long> Subcategories);
public sealed record SubcategoryListItemDto(long Id, string Name);
public sealed record CategoryListItemDto(long Id, string Name, string? IconUrl, List<SubcategoryListItemDto> Subcategories);
