﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobland.Models;

public class Entity
{
    public long Id { get; set; }
    public override string ToString() => $"{GetType().Name} #{Id.ToString()}";
    public override int GetHashCode() => Id.GetHashCode();
}

public class AuditableEntity : Entity
{
    public DateTime? Added { get; set; }
    public DateTime? Modified { get; set; }
}

public sealed class Category : AuditableEntity
{
    [MaxLength(100)] public string Name { get; set; } = "";
    [MaxLength(255)] public string? IconUrl { get; set; }
    public HashSet<Subcategory> Subcategories { get; } = new();
    public override string ToString() => Name;
}

public sealed class Subcategory : AuditableEntity
{
    [MaxLength(100)] public string Name { get; set; } = "";
    public long? CategoryId { get; set; }
    public Category? Category { get; set; }
    public override string ToString() => Name;
}

public sealed class Work : AuditableEntity
{
    [MaxLength(100)] public string Title { get; set; } = "";
    [MaxLength(65535)] public string? Description { get; set; } = "";
    public DateTime? StartedOn { get; set; }
    public DateTime? FinishedOn { get; set; }
    [MaxLength(30)] public string? PhoneNumber { get; set; } = "";
    public long? LowerPriceBound { get; set; }
    public long? UpperPriceBound { get; set; }
    public Category? Category { get; set; }
    public long CategoryId { get; set; }
    public Subcategory? Subcategory { get; set; }
    public long SubcategoryId { get; set; }
    [MaxLength(255)] public string AuthorId { get; set; } = "";
    public override string ToString() => Title;
}

public sealed class WorkDetails : Entity
{
    public long? Responses { get; private set; } = 0; 
    public long WorkId { get; set; }
    public Work? Work { get; set; }
    public WorkDetails IncrementResponses()
    {
        Responses += 1;
        return this;
    }

    public override string ToString() => Responses.GetValueOrDefault(-1).ToString();
}
