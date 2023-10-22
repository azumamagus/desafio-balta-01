using System.Text;

namespace Balta.IBGE.Domain.Core.Extensions;

public static class StringExtesions
{
    public static string ToBase64(this string text)
       => Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
    public static bool IsNullOrWhiteSpace(this string? source)
        => string.IsNullOrWhiteSpace(source) || string.IsNullOrEmpty(source);
}
