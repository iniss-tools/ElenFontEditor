using System.Drawing.Text;
using System.Runtime.InteropServices;
using ElenFontEditor.Tools;

namespace ElenFontEditor.Import;

/// <summary>
/// Pismo Windows na import: nainstalovane alebo zo suboru .ttf/.otf (subor sa neinstaluje, nacita sa len pre program).
/// </summary>
internal sealed class TrueTypeSource : IDisposable
{
    private readonly PrivateFontCollection? _collection;

    // subor pisma pridany aj do GDI (len pre tento proces) - GDI podla neho zistuje, ktore znaky pismo ma
    private readonly string? _gdiFile;

    private TrueTypeSource(FontFamily family, PrivateFontCollection? collection, string? gdiFile)
    {
        Family = family;
        _collection = collection;
        _gdiFile = gdiFile;
    }

    /// <summary>Rodina pisma.</summary>
    public FontFamily Family { get; }

    /// <summary>
    /// Nainstalovane pismo s nazvom <paramref name="name" />.
    /// </summary>
    public static TrueTypeSource Installed(string name) => new(new FontFamily(name), null, null);

    /// <summary>
    /// Pismo zo suboru. Subor, ktory GDI+ nevie nacitat (napr. OpenType s krivkami CFF), hodi <see cref="FormatException" />.
    /// </summary>
    public static TrueTypeSource FromFile(string path)
    {
        var collection = new PrivateFontCollection();
        try
        {
            collection.AddFontFile(path);
        }
        catch (Exception e) when (e is ArgumentException or ExternalException)
        {
            collection.Dispose();
            throw new FormatException(e.Message, e);
        }

        if (collection.Families.Length == 0)
        {
            collection.Dispose();
            throw new FormatException(path);
        }

        var gdiFile = Win32.AddFontResourceEx(path, Win32.FrPrivate, IntPtr.Zero) > 0 ? path : null;
        return new TrueTypeSource(collection.Families[0], collection, gdiFile);
    }

    /// <summary>
    /// Rez, ktory pismo ma: pozadovany, inak obycajny a nakoniec prvy dostupny.
    /// </summary>
    public FontStyle AvailableStyle(FontStyle wanted)
    {
        if (Family.IsStyleAvailable(wanted))
            return wanted;
        foreach (var style in new[] { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic, FontStyle.Bold | FontStyle.Italic })
        {
            if (Family.IsStyleAvailable(style))
                return style;
        }

        return wanted;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_collection is null)
            Family.Dispose();
        _collection?.Dispose();
        if (_gdiFile is not null)
            Win32.RemoveFontResourceEx(_gdiFile, Win32.FrPrivate, IntPtr.Zero);
    }
}
