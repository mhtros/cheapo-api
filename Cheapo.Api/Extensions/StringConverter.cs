namespace Cheapo.Api.Extensions;

public static class StringConverter
{
    public static string? ToUnderscore(this string? str)
    {
        return str?.ToUpper().Replace(' ', '_').Trim('.');
    }
}