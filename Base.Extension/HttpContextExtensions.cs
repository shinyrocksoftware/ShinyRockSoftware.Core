using Microsoft.AspNetCore.Http;

namespace Base.Extension;

public static class HttpContextExtensions
{
    public static string GetRequestId(this HttpContext source)
    {
        return source?.TraceIdentifier ?? Guid.NewGuid().ToString();
    }
}