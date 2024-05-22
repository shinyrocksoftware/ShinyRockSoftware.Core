namespace Core.Extension;

public static class StreamExtensions
{
    public static string ToImageBase64(this Stream stream, string contentType)
    {
        using var memoryStream = new MemoryStream();
        stream.Position = 0;
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray().BinaryToImageBase64(contentType);
    }

    public static string ReadToEnd(this Stream source)
    {
        using var streamReader = new StreamReader(source);
        return streamReader.ReadToEnd();
    }
}