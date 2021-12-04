namespace Jobland.Domain.Common;

public abstract class Entity
{
    public long Id { get; set; }
    public override string ToString() => $"{GetType().Name} #{Id.ToString()}";
    public override int GetHashCode() => Id.GetHashCode();
}
