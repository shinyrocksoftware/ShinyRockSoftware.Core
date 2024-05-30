namespace Base.Extension;

public static class IntExtensions
{
	public static Guid ToGuid(this uint source)
	{
		byte[] bytes = new byte[16];
		BitConverter.GetBytes(source).CopyTo(bytes, 0);
		return new(bytes);
	}
}