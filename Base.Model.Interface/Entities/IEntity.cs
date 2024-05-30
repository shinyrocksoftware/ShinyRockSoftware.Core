namespace Base.Model.Interface.Entities;

public interface IEntity<T> : IPlainEntity<T>
{
	bool? IsActive { get; set; }
	DateTime CreatedAt { get; set; }
	Guid CreatedBy { get; set; }
	DateTime? LastModifiedAt { get; set; }
	Guid? LastModifiedBy { get; set; }
	DateTime? DeletedAt { get; set; }
	Guid? DeletedBy { get; set; }
}