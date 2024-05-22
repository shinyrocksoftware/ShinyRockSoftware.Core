namespace Core.Extension;

public static class ObjectExtensions
{
    public static T As<T>(this object source)
    {
        return (T)source;
    }

    public static byte[] ToByteArray(this object source)
    {
        var size = Marshal.SizeOf(source);
        var bytes = new byte[size];
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(source, ptr, false);
        Marshal.Copy(ptr, bytes, 0, size);
        Marshal.FreeHGlobal(ptr);

        return bytes;
    }
}