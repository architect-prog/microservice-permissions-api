namespace Microservice.Permissions.Core.Extensions;

public static class FormattingExtensions
{
    public static string ToStringSequence<T>(this IEnumerable<T>? source, string delimiter = "-")
    {
        if (source is null)
            return string.Empty;

        var result = string.Join(delimiter, source);
        return result;
    }
}