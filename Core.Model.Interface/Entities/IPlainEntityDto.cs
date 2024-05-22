namespace Core.Model.Interface.Entities;

public interface IPlainEntityDto<T>
{
	T Id { get; set; }
	byte[] RowVersion { get; set; }
}