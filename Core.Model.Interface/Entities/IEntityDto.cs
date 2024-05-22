namespace Core.Model.Interface.Entities;

public interface IEntityDto<T> : IPlainEntityDto<T>
{
	bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	DateTime? LastModifiedAt { get; set; }
	Guid? LastModifiedBy { get; set; }
	DateTime? DeletedAt { get; set; }
	Guid? DeletedBy { get; set; }
}