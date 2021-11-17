namespace Jobland.Dtos;

public record CategoryAddRequest(string Name, string? IconUrl);
public record CategoryUpdateRequest(long Id, string Name, string? IconUrl, Dictionary<long, HashSet<long>> Ids);
