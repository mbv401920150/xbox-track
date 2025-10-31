using System.Text;
using System.Text.RegularExpressions;

namespace XboxTrack.Helpers;

public static class ExtensionHelper
{
    public static string RemoveSpecialChar(this string value) =>
        value.Replace("\u00b4", "'") // ´
            .Replace("\u00ae", "") // ®
            .Replace("\u24c7", "") // Ⓡ
            .Replace("\u2122", ""); // ™

    public static string RemoveAccents(this string value)
    {
        var normalizedString = value.Normalize(NormalizationForm.FormD);
        var nonAccentString = Regex.Replace(normalizedString, @"\p{M}", "");
        return nonAccentString;
    }
}