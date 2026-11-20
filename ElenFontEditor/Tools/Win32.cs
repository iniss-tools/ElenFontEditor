using System.Runtime.InteropServices;

namespace ElenFontEditor.Tools;

/// <summary>
/// Volania GDI pre import pisma Windows: ktore znaky pismo ma a pismo zo suboru len pre tento program.
/// </summary>
internal static partial class Win32
{
    /// <summary>GetGlyphIndices: znak, ktory pismo nema, dostane index 0xFFFF.</summary>
    internal const uint GgiMarkNonexistingGlyphs = 1;

    /// <summary>Chybajuci znak pri <see cref="GgiMarkNonexistingGlyphs" />.</summary>
    internal const ushort MissingGlyph = 0xFFFF;

    /// <summary>AddFontResourceEx: pismo vidi len tento proces, po skonceni zmizne.</summary>
    internal const uint FrPrivate = 0x10;

    [LibraryImport("gdi32.dll", EntryPoint = "GetGlyphIndicesW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial uint GetGlyphIndices(IntPtr hdc, string text, int count, [Out] ushort[] indices, uint flags);

    [LibraryImport("gdi32.dll")]
    internal static partial IntPtr SelectObject(IntPtr hdc, IntPtr gdiObject);

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool DeleteObject(IntPtr gdiObject);

    [LibraryImport("gdi32.dll", EntryPoint = "AddFontResourceExW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial int AddFontResourceEx(string path, uint flags, IntPtr reserved);

    [LibraryImport("gdi32.dll", EntryPoint = "RemoveFontResourceExW", StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool RemoveFontResourceEx(string path, uint flags, IntPtr reserved);
}
