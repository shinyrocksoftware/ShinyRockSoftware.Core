using Microsoft.AspNetCore.Http;

namespace Core.Extension;

public static class HttpContextExtensions
{
    public static string GetRequestId(this HttpContext source)
    {
        return source?.TraceIdentifier ?? Guid.NewGuid().ToString();
    }
}