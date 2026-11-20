using System.Globalization;
using ElenFontEditor.Entities;
using ElenFontEditor.Properties;
using ElenFontEditor.Tools;
using ElenFontEditor.XML;
using ToolsCore.Iniss.Elen;
using ToolsCore.Iniss.Tools;

namespace ElenFontEditor.Controls;

/// <summary>
/// Panel Nahlad na tabuli: ukazkovy text upravovanym pismom na bodovej tabuli ELEN tak, ako ho kresli panel -
/// aj s neulozenymi zmenami. Upravovany znak je v texte oramovany, klik na znak ho vyberie v zozname znakov.
/// </summary>
public sealed partial class BoardPreview : UserControl
{
    // najvacsia vzdialenost bodov - kratky text by inak mal obrovske body
    private const int MaxPitch = 12;

    private BoardPreviewSettings _settings = new();
    private TableFont? _font;
    private SymbolCharset _charset;
    private string _emptyText = "";
    private bool _loading;

    public BoardPreview()
    {
        InitializeComponent();
        board.MarkMissing = true;
        board.MaxPitch = MaxPitch;
        board.Cursor = Cursors.Hand;
        board.GlyphClick += Board_GlyphClick;
        cbColor.DisplayMember = nameof(KeyValuePair<int, string>.Value);
        cbColor.ValueMember = nameof(KeyValuePair<int, string>.Key);
        cbColor.DataSource = new List<KeyValuePair<int, string>>
        {
            new(0, Resources.BoardPreview_Color0),
            new(1, Resources.BoardPreview_Color1),
            new(2, Resources.BoardPreview_Color2),
            new(3, Resources.BoardPreview_Color3)
        };
    }

    /// <summary>
    /// Klik na znak textu - kod znaku v pisme.
    /// </summary>
    public event EventHandler<int>? SymbolClick;

    /// <summary>
    /// Zobrazovany riadok (pre testy a snimky).
    /// </summary>
    internal ElenTextRaster? Raster => board.Raster;

    /// <summary>
    /// Nacita ukazkovy text a vzhlad z nastaveni; zmeny sa do nich zapisuju hned.
    /// </summary>
    public void LoadSettings(BoardPreviewSettings settings)
    {
        _settings = settings;
        _loading = true;
        try
        {
            tbText.Text = settings.Text ?? Resources.BoardPreview_DefaultText;
            cbColor.SelectedValue = Math.Clamp(settings.Color, 0, 3);
            cbTall.Checked = settings.TallDigits;
            cbWidth.Checked = settings.Width > 0;
            nudWidth.Value = Math.Clamp(settings.Width > 0 ? settings.Width : 128, (int)nudWidth.Minimum, (int)nudWidth.Maximum);
            nudWidth.Enabled = cbWidth.Checked;
        }
        finally
        {
            _loading = false;
        }

        Render();
    }

    /// <summary>
    /// Pismo, ktorym sa text kresli, a kodovanie textu podla nastavenia stlpca Znak. Volat po kazdej zmene pisma -
    /// aj neulozenej.
    /// </summary>
    public void ShowFont(TableFont font, SymbolCharset charset)
    {
        _font = font;
        _charset = charset;
        _emptyText = "";
        Render();
    }

    /// <summary>
    /// Prazdna tabula bez pisma (ziadny otvoreny subor alebo subor bez pisiem) s vysvetlenim pod nou.
    /// </summary>
    public void Clear(string emptyText)
    {
        _font = null;
        _emptyText = emptyText;
        Render();
    }

    /// <summary>
    /// Oramuje upravovany znak s kodom <paramref name="code" />; -1 = ziadny.
    /// </summary>
    public void HighlightSymbol(int code) => board.HighlightCode = code;

    /// <summary>
    /// Kody textu v kodovani <paramref name="charset" />; znaky, ktore kodovanie nema, idu do <paramref name="unsupported" />.
    /// </summary>
    internal static byte[] Encode(string text, SymbolCharset charset, ICollection<char> unsupported)
    {
        var encoding = charset == SymbolCharset.Kamenicky ? Encodings.Kamenicky : Encodings.Win1250;
        var codes = new List<byte>(text.Length);
        foreach (var c in text)
        {
            var bytes = encoding.GetBytes(c.ToString());
            // znak, ktory sa nezakoduje spat na seba (nahradny '?'), na tabulu nejde
            if (bytes.Length == 1 && (c == ' ' || SymbolChars.Text(bytes[0], charset) == c.ToString()))
                codes.Add(bytes[0]);
            else if (!unsupported.Contains(c))
                unsupported.Add(c);
        }

        return codes.ToArray();
    }

    private void Render()
    {
        if (_loading)
            return;

        board.DotColor = cbColor.SelectedValue is int color ? color : 3;
        board.BoardColumns = cbWidth.Checked ? (int)nudWidth.Value : 0;
        if (_font is null)
        {
            board.Raster = null;
            lInfo.Text = _emptyText;
            return;
        }

        var missing = new List<char>();
        var codes = Encode(tbText.Text, _charset, missing);
        var raster = ElenText.Render(ElenBinParser.ToElenFont(_font), codes, cbTall.Checked);
        // bez sirky tabule sa tabula prisposobi textu (bod volneho miesta po oboch stranach)
        if (!cbWidth.Checked)
            board.BoardColumns = raster.Width + 2;
        board.BoardRows = raster.Height;
        board.Raster = raster;

        foreach (var glyph in raster.Glyphs.Where(glyph => glyph.IsMissing))
        {
            if (SymbolChars.Text(codes[glyph.Index], _charset) is [var c] && !missing.Contains(c))
                missing.Add(c);
        }

        var info = string.Format(CultureInfo.CurrentCulture, Resources.BoardPreview_TextWidth, raster.Width);
        if (cbWidth.Checked && raster.Width + board.TextLeft > nudWidth.Value)
            info += " " + string.Format(CultureInfo.CurrentCulture, Resources.BoardPreview_TooWide, (int)nudWidth.Value);
        if (missing.Count > 0)
            info += " " + string.Format(CultureInfo.CurrentCulture, Resources.BoardPreview_Missing, string.Join(" ", missing));
        lInfo.Text = info;
    }

    private void Board_GlyphClick(object? sender, int index)
    {
        if (board.Raster is { } raster && index < raster.Glyphs.Count)
            SymbolClick?.Invoke(this, raster.Glyphs[index].Code);
    }

    private void tbText_TextChanged(object? sender, EventArgs e)
    {
        if (_loading)
            return;
        _settings.Text = tbText.Text;
        Render();
    }

    private void cbColor_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_loading || cbColor.SelectedValue is not int color)
            return;
        _settings.Color = color;
        Render();
    }

    private void cbTall_CheckedChanged(object? sender, EventArgs e)
    {
        if (_loading)
            return;
        _settings.TallDigits = cbTall.Checked;
        Render();
    }

    private void cbWidth_CheckedChanged(object? sender, EventArgs e)
    {
        nudWidth.Enabled = cbWidth.Checked;
        if (_loading)
            return;
        _settings.Width = cbWidth.Checked ? (int)nudWidth.Value : 0;
        Render();
    }

    private void nudWidth_ValueChanged(object? sender, EventArgs e)
    {
        if (_loading || !cbWidth.Checked)
            return;
        _settings.Width = (int)nudWidth.Value;
        Render();
    }
}
