using Jobland.Domain.Common;

namespace Jobland.Domain.Core;

/// <summary>
/// represents work request in public employment service.
/// </summary>
public sealed class Work : AuditableEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime? StartedOn { get; set; }
    public DateTime? FinishedOn { get; set; }
    public string PhoneNumber { get; set; } = "";
    public long? LowerPriceBound { get; set; }
    public long? UpperPriceBound { get; set; }
    public Category? Category => Subcategory?.Category;
    public Subcategory? Subcategory { get; set; }
    public long SubcategoryId { get; set; }
    public string AuthorId { get; set; } = "";
    public long ResponseCount { get; private set; }

    public Work IncrementResponses()
    {
        ResponseCount += 1;
        return this;
    }
    public override string ToString() => Title;
}
