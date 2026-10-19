using System.Globalization;
using ElenFontEditor.Properties;
using ToolsCore.Iniss.Elen;

namespace ElenFontEditor.Entities;

/// <summary>
/// Kontrola pisiem v sprave pisiem - vracia text chyby pre obsluhu, okno ho ukaze.
/// </summary>
internal static class FontRules
{
    /// <summary>Najmensia sirka a vyska pisma v bodoch.</summary>
    public const int MinSize = 1;

    /// <summary>
    /// Chyba nazvu pisma alebo null. Medzery na okrajoch sa nerataju - okno ich oreze (subor ich na konci nezachova).
    /// </summary>
    /// <param name="name">nazov pisma</param>
    /// <param name="otherNames">nazvy ostatnych pisiem suboru (subor rovnomenne pismo pri citani vynecha)</param>
    public static string? NameError(string? name, IEnumerable<string> otherNames)
    {
        var text = name?.Trim() ?? "";
        if (text.Length == 0)
            return Resources.FontManager_NameEmpty;
        if (text.Length > TableFont.NameMaxLength)
            return string.Format(CultureInfo.CurrentCulture, Resources.FontManager_NameLength, TableFont.NameMaxLength);
        // subor uklada nazov v ASCII
        if (!text.All(ch => ch is >= ' ' and <= '~'))
            return Resources.FontManager_NameAscii;
        if (otherNames.Any(other => string.Equals(other.Trim(), text, StringComparison.Ordinal)))
            return string.Format(CultureInfo.CurrentCulture, Resources.FontManager_NameDuplicate, text);
        return null;
    }

    /// <summary>
    /// Chyba rozmeru pisma - sirka alebo vyska v bodoch od <see cref="MinSize" /> do <paramref name="max" /> - alebo null.
    /// </summary>
    public static string? SizeError(string? text, int max) =>
        int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out var value) && value >= MinSize && value <= max
            ? null
            : string.Format(CultureInfo.CurrentCulture, Resources.FontManager_SizeRange, MinSize, max);

    /// <summary>
    /// Prva chyba pisiem pred ulozenim alebo null.
    /// </summary>
    public static string? FindProblem(IReadOnlyList<TableFont> fonts)
    {
        if (fonts.Count > ElenFontFile.MaxFonts)
            return string.Format(CultureInfo.CurrentCulture, Resources.FontManager_TooManyFonts, ElenFontFile.MaxFonts);

        for (var i = 0; i < fonts.Count; i++)
        {
            if (NameError(fonts[i].Name, fonts.Take(i).Select(font => font.Name)) is { } error)
                return error;
        }

        return null;
    }
}
