namespace Jobland.Dtos;

public sealed record CategoryAddRequest(string Name, string? IconUrl);
public sealed record CategoryUpdateRequest(long Id, string? Name, string? IconUrl, HashSet<long> Subcategories);
public sealed record SubcategoryListItemDto(long Id, string Name);
public sealed record CategoryListItemDto(long Id, string Name, string? IconUrl, List<SubcategoryListItemDto> Subcategories);
public sealed record WorkAddRequest(string Title, string Description, DateTime? StartedOn,
    DateTime? FinishedOn,
    string PhoneNumber, long? LowerPriceBound, long? UpperPriceBound, long SubcategoryId);
public sealed record CategoryDto(long Id, string Name, string? IconUrl);
public sealed record WorkDto(long Id, string Title, string Description, DateTime? StartedOn,
    DateTime? FinishedOn,
    string PhoneNumber, long? LowerPriceBound, long? UpperPriceBound,
    CategoryDto Category, SubcategoryListItemDto Subcategory, DateTime? Added, DateTime? Modified, string AuthorId, long? ResponseCount);
public sealed record WorkCountDto(int Count);

