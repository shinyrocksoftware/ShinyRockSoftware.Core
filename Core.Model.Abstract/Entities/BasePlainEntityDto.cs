namespace Core.Model.Abstract.Entities;

public abstract class BasePlainEntityDto<T>
{
	public T Id { get; set; }
	public byte[] RowVersion { get; set; }
}