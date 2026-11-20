using System.Drawing.Text;
using ElenFontEditor.Tools;

namespace ElenFontEditor.Import;

/// <summary>
/// Kresli znaky pisma Windows do bodov: vyhladene v stvornasobnom rozliseni s prahom pokrytia alebo bodovo
/// s hintingom pisma (pismo samo zarovna tahy na body).
/// </summary>
internal sealed class TrueTypeRasterizer : IDisposable
{
    private const int Supersample = 4;

    private readonly TrueTypeOptions _options;
    private readonly int _rows;
    private readonly int _columns;
    private readonly int _margin;
    private readonly int _scale;
    private readonly Font _font;
    private readonly Bitmap _canvas;
    private readonly Graphics _graphics;
    private readonly StringFormat _format;
    private readonly float _top;
    private readonly IntPtr _hfont;

    /// <param name="family">rodina pisma</param>
    /// <param name="options">vykreslenie</param>
    /// <param name="rows">vyska pisma ELEN v bodoch</param>
    public TrueTypeRasterizer(FontFamily family, TrueTypeOptions options, int rows)
    {
        ArgumentNullException.ThrowIfNull(family);
        ArgumentNullException.ThrowIfNull(options);
        _options = options;
        _rows = rows;
        // okraj vlavo pre znaky siahajuce pred zaciatok (kurziva, zaporny lavy okraj)
        _margin = Math.Max(4, rows);
        _columns = _margin * 2 + Math.Max(16, (int)Math.Ceiling(options.Size * 2));
        _scale = options.Smooth ? Supersample : 1;

        _font = new Font(family, Math.Max(1f, options.Size) * _scale, options.Style, GraphicsUnit.Pixel);
        _canvas = new Bitmap(_columns * _scale, rows * _scale);
        _graphics = Graphics.FromImage(_canvas);
        _graphics.TextRenderingHint = options.Smooth ? TextRenderingHint.AntiAlias : TextRenderingHint.SingleBitPerPixelGridFit;
        _format = (StringFormat)StringFormat.GenericTypographic.Clone();
        _format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap;

        // DrawString kresli riadok od vrchu bunky - uciarie je o vystup pisma nizsie
        var ascent = _font.Size * family.GetCellAscent(options.Style) / family.GetEmHeight(options.Style);
        _top = options.Baseline * _scale - ascent;
        _hfont = _font.ToHfont();
    }

    /// <summary>
    /// Body znaku [riadok, stlpec] a jeho sirka v bodoch; null = pismo znak nema.
    /// </summary>
    public (bool[,] Ink, int Advance)? Render(char c)
    {
        // GDI+ by chybajuci znak nakreslil nahradnym pismom - preto sa zisti z tabulky znakov pisma
        if (!HasGlyph(c))
            return null;

        var ink = Draw(c);
        var advance = _graphics.MeasureString(c.ToString(), _font, PointF.Empty, _format).Width / _scale;
        return (ink, (int)Math.Round(advance));
    }

    private bool[,] Draw(char c)
    {
        _graphics.Clear(Color.Black);
        _graphics.DrawString(c.ToString(), _font, Brushes.White, _margin * _scale, _top, _format);

        var coverage = PixelCoverage.FromImage(_canvas, darkInk: false);
        var threshold = _options.Smooth ? Math.Clamp(_options.Threshold, 1, 99) / 100f : 0.5f;
        var dots = new bool[_rows, _columns];
        for (var r = 0; r < _rows; r++)
        {
            for (var col = 0; col < _columns; col++)
                dots[r, col] = coverage.Average(col * _scale, r * _scale, _scale) >= threshold;
        }

        return dots;
    }

    /// <summary>
    /// Ci pismo znak ma (GDI GetGlyphIndices nad tym istym pismom).
    /// </summary>
    public bool HasGlyph(char c)
    {
        var hdc = _graphics.GetHdc();
        try
        {
            var previous = Win32.SelectObject(hdc, _hfont);
            var indices = new ushort[1];
            var result = Win32.GetGlyphIndices(hdc, c.ToString(), 1, indices, Win32.GgiMarkNonexistingGlyphs);
            Win32.SelectObject(hdc, previous);
            // pri chybe GDI (GDI_ERROR) sa znak nevynechava
            return result == uint.MaxValue || indices[0] != Win32.MissingGlyph;
        }
        finally
        {
            _graphics.ReleaseHdc(hdc);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Win32.DeleteObject(_hfont);
        _format.Dispose();
        _graphics.Dispose();
        _canvas.Dispose();
        _font.Dispose();
    }
}
