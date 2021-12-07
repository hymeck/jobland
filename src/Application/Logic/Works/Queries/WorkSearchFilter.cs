namespace Jobland.Application.Logic.Works.Queries;

public readonly struct WorkSearchFilter
{
    public string? Title { get; init; }
    public long? LowerPriceBound { get; init; }
    public long? UpperPriceBound { get; init; }
    public DateTime? Started { get; init; }
    public DateTime? Finished { get; init; }
    /// <summary>
    /// When <c>false</c>, indicates only <see cref="Jobland.Domain.Core.Work"/> objects with ResponseCount == 0 should be included in result list.
    /// </summary>
    public bool? WithResponses { get; init; }
    public HashSet<long>? Subcategories { get; init; }
}
