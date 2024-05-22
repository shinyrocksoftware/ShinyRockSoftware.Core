namespace Core.Model.Abstract.Entities;

public abstract class BaseEntity<T> : BasePlainEntity<T>
{
    public bool? IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime? LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
}