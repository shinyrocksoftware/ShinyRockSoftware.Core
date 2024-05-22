using System.Text.Json.Serialization;

namespace Core.Model.Abstract.Entities;

public abstract class BaseEntityDto<T> : BasePlainEntityDto<T>
{
    [JsonIgnore]
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
}