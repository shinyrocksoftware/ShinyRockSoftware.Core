using System.Diagnostics;
using Core.Extension;

namespace Core.Model.Extensions;

public static class ExceptionExtensions
{
    public static IList<string> GetExceptionDetails(this Exception exception)
    {
        var result = new List<string>();

        var innerEx = exception;

        while (innerEx != null)
        {
            var message = innerEx.Message;
            result.Add(message);

            innerEx = innerEx.InnerException;
        }

        return result;
    }
    
    /// <summary>
    ///     Gets the entire stack trace consisting of exception's footprints (File, Method, LineNumber)
    /// </summary>
    /// <param name="exception">Source <see cref="Exception" /></param>
    /// <returns>
    ///     <see cref="string" /> that represents the entire stack trace consisting of exception's footprints (File, Method, LineNumber)
    /// </returns>
    public static IList<object> GetExceptionStackTraceFootprints(this Exception exception)
    {
        var result = new List<object>();

        var stackTrace = new StackTrace(exception, true);
        var frames = stackTrace.GetFrames();
        var middleMethodsStringBuilder = new StringBuilder();

        for (int i = 0, j = 0; i < frames.Length; i++, j++)
        {
            var frame = frames[i];

            if (frame.GetFileLineNumber() < 1)
            {
                continue;
            }

            for (int k = 0; k < j; k++)
            {
                middleMethodsStringBuilder.Append(".");
            }

            if (i > 0)
            {
                result.Add(middleMethodsStringBuilder);

                middleMethodsStringBuilder = new();
            }

            var exceptionFootprint = new ExceptionFootprint
            {
                File = frame.GetFileName()
            };

            string? method;
            var methodDeclaringType = frame.GetMethod()?.DeclaringType;
            if (methodDeclaringType == null)
            {
                method = frame.GetMethod()?.Name;
            }
            else
            {
                var fullMethodName = methodDeclaringType.FullName;
                method = fullMethodName?.ExtractRegex("^(.*)(\\+)<(\\w*)>(.*)$", 3);
            }

            exceptionFootprint.Method = method;
            exceptionFootprint.LineNumber = frame.GetFileLineNumber();

            result.Add(exceptionFootprint);

            j = 0;

            if (i == frames.Length - 1)
            {
                break;
            }
        }

        return result;
    }
}