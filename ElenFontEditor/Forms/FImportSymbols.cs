using System.Drawing.Text;
using System.Globalization;
using ElenFontEditor.Controls;
using ElenFontEditor.Entities;
using ElenFontEditor.Import;
using ElenFontEditor.Properties;
using ElenFontEditor.Tools;
using Serilog;
using ToolsCore.Iniss.Elen;
using ToolsCore.Tools;
using Utils = ToolsCore.Tools.Utils;

namespace ElenFontEditor.Forms;

/// <summary>
/// Import znakov z pisma Windows alebo z obrazka do noveho alebo existujuceho pisma. Vysledok sa uklada do kopie
/// pisma (<see cref="Result" />) - subor zmeni az hlavne okno po potvrdeni, takze Zrusit nic nemeni.
/// </summary>
public partial class FImportSymbols : Form
{
    private const string DefaultFamily = "Arial";

    private readonly FontsFile _file;
    private readonly ImportSource _source;
    private readonly SymbolCharset _charset;
    private readonly List<Bitmap> _images = [];
    private TrueTypeSource? _installedFont;
    private TrueTypeSource? _fileFont;
    private Bitmap? _image;
    private string? _imagePath;
    private int _lastHeight;
    private bool _loading = true;

    // dialog vyberu suboru este nedobehol
    private bool _choosingFile;
    private List<int> _imported = [];

    /// <param name="file">otvoreny subor pisiem</param>
    /// <param name="source">pismo Windows alebo obrazok</param>
    /// <param name="fontIndex">vybrane pismo v hlavnom okne (predvoleny ciel)</param>
    /// <param name="code">vybrany znak (kod jedneho znaku z obrazka)</param>
    /// <param name="charset">kodovanie stlpca Znak - podla neho sa kod prevedie na znak pisma Windows</param>
    public FImportSymbols(FontsFile file, ImportSource source, int fontIndex, int code, SymbolCharset charset)
    {
        InitializeComponent();
        _file = file;
        _source = source;
        _charset = charset;

        Text = source == ImportSource.Font ? Resources.Import_TitleFont : Resources.Import_TitleImage;
        gbFont.Visible = source == ImportSource.Font;
        gbImage.Visible = source == ImportSource.Image;
        InitGrid();
        InitTarget(fontIndex);
        if (source == ImportSource.Font)
            InitFont();
        else
            InitImage(code);

        boardSample.MarkMissing = true;
        boardSample.MaxPitch = 8;
        boardSample.DotColor = 3;
        tbSample.Text = Resources.BoardPreview_DefaultText;
        this.ApplyThemeAndFonts();
        _loading = false;
        UpdateModeControls();
        UpdateResult();
    }

    /// <summary>Pismo s importovanymi znakmi (kopia - subor sa nemeni); null = nie je co importovat.</summary>
    public TableFont? Result { get; private set; }

    /// <summary>Pismo suboru, ktore vysledok nahradi; -1 = nove pismo na konci suboru.</summary>
    public int TargetIndex => rbNewFont.Checked ? -1 : cbTargetFont.SelectedIndex;

    /// <summary>Pocet importovanych znakov.</summary>
    public int ImportedCount => _imported.Count;

    /// <summary>Nazov zdroja do popisu kroku spat: pismo alebo meno suboru obrazka.</summary>
    public string SourceName => _source == ImportSource.Font
        ? CurrentFont?.Family.Name ?? ""
        : Path.GetFileName(_imagePath) ?? "";

    private void InitGrid()
    {
        dgvResult.AutoGenerateColumns = false;
        dgvResult.Columns.AddRange(
            new DataGridViewTextBoxColumn { DataPropertyName = nameof(ResultRow.Code), HeaderText = Resources.Import_ColCode, Width = 45 },
            new DataGridViewTextBoxColumn { DataPropertyName = nameof(ResultRow.Char), HeaderText = Resources.Import_ColChar, Width = 45 },
            new DataGridViewImageColumn { DataPropertyName = nameof(ResultRow.New), HeaderText = Resources.Import_ColNew, Width = 60 },
            new DataGridViewTextBoxColumn { DataPropertyName = nameof(ResultRow.Width), HeaderText = Resources.Import_ColWidth, Width = 50 },
            new DataGridViewImageColumn { DataPropertyName = nameof(ResultRow.Old), HeaderText = Resources.Import_ColOld, Width = 60 },
            new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ResultRow.Note), HeaderText = Resources.Import_ColNote,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
    }

    private void InitTarget(int fontIndex)
    {
        var current = fontIndex >= 0 && fontIndex < _file.Fonts.Count ? _file.Fonts[fontIndex] : null;
        tbNewName.Text = FFontManager.CreateNewFont(_file.Fonts).Name;
        nudNewWidth.Value = Math.Clamp(current?.MaxWidth ?? 8, (int)nudNewWidth.Minimum, (int)nudNewWidth.Maximum);
        nudNewHeight.Value = Math.Clamp(current?.Height ?? 16, (int)nudNewHeight.Minimum, (int)nudNewHeight.Maximum);
        cbNewProportional.Checked = true;
        cbCenter.Checked = true;

        cbTargetFont.DataSource = _file.Fonts.Select(font => font.Name).ToList();
        rbExisting.Enabled = _file.Fonts.Count > 0;
        rbNewFont.Enabled = _file.Fonts.Count < ElenFontFile.MaxFonts;
        if (current is not null)
        {
            cbTargetFont.SelectedIndex = fontIndex;
            rbExisting.Checked = true;
        }
        else
        {
            rbNewFont.Checked = true;
        }
    }

    private void InitFont()
    {
        cbRender.Items.AddRange([Resources.Import_RenderSmooth, Resources.Import_RenderHinted]);
        cbRender.SelectedIndex = 0;

        using var installed = new InstalledFontCollection();
        var names = installed.Families.Select(family => family.Name).ToList();
        cbFamily.DataSource = names;
        var index = names.FindIndex(name => string.Equals(name, DefaultFamily, StringComparison.OrdinalIgnoreCase));
        cbFamily.SelectedIndex = Math.Max(0, index);
        rbInstalled.Checked = true;
        LoadInstalledFont();
        ApplyFontDefaults();

        nudFrom.Value = 32;
        nudTo.Value = 255;
    }

    private void InitImage(int code)
    {
        rbSingle.Checked = true;
        nudFrom.Value = Math.Clamp(code, 0, 255);
        nudTo.Value = 255;
        // jeden znak z obrazka ma obvykle kresbu od laveho okraja - stred by ho posunul
        cbCenter.Checked = false;
        nudSpacing.Value = 1;
    }

    /// <summary>Pismo Windows podla volby: nainstalovane alebo zo suboru.</summary>
    private TrueTypeSource? CurrentFont => rbFontFile.Checked ? _fileFont : _installedFont;

    /// <summary>Vyska cieloveho pisma.</summary>
    private int TargetHeight => rbNewFont.Checked
        ? (int)nudNewHeight.Value
        : cbTargetFont.SelectedIndex >= 0 ? _file.Fonts[cbTargetFont.SelectedIndex].Height : (int)nudNewHeight.Value;

    private ImportTarget? Target()
    {
        if (rbNewFont.Checked)
            return new ImportTarget(-1, tbNewName.Text.Trim(), (int)nudNewWidth.Value, (int)nudNewHeight.Value, cbNewProportional.Checked, false);
        return cbTargetFont.SelectedIndex >= 0 ? new ImportTarget(cbTargetFont.SelectedIndex, "", 0, 0, false, cbOnlyEmpty.Checked) : null;
    }

    private GlyphLayout GlyphLayoutFor(ImportTarget target)
    {
        var font = target.IsNewFont ? null : _file.Fonts[target.FontIndex];
        return new GlyphLayout(font?.Height ?? target.Height, font?.MaxWidth ?? target.MaxWidth, font?.IsProportional ?? target.Proportional,
            (int)nudSpacing.Value, (int)nudIndent.Value, cbCenter.Checked);
    }

    /// <summary>Velkost a uciarie pisma Windows tak, aby sa cele zmestilo do vysky cieloveho pisma.</summary>
    private void ApplyFontDefaults()
    {
        if (CurrentFont is not { } font)
            return;

        var height = TargetHeight;
        var (size, baseline) = TrueTypeOptions.Defaults(font.Family, font.AvailableStyle(SelectedStyle()), height);
        var loading = _loading;
        _loading = true;
        try
        {
            nudSize.Value = Math.Clamp(size, (int)nudSize.Minimum, (int)nudSize.Maximum);
            nudBaseline.Maximum = Math.Max(1, height);
            nudBaseline.Value = Math.Clamp(baseline, (int)nudBaseline.Minimum, (int)nudBaseline.Maximum);
        }
        finally
        {
            _loading = loading;
        }

        _lastHeight = height;
    }

    private FontStyle SelectedStyle() =>
        (cbBold.Checked ? FontStyle.Bold : FontStyle.Regular) | (cbItalic.Checked ? FontStyle.Italic : FontStyle.Regular);

    private void LoadInstalledFont()
    {
        if (cbFamily.SelectedItem is not string name)
            return;
        _installedFont?.Dispose();
        _installedFont = TrueTypeSource.Installed(name);
    }

    /// <summary>Volby, ktore zavisia od rezimu: rozsah pre jeden znak z obrazka a mriezka.</summary>
    private void UpdateModeControls()
    {
        var single = _source == ImportSource.Image && rbSingle.Checked;
        lFrom.Text = single ? Resources.Import_CodeLabel : Resources.Import_FromLabel;
        lTo.Visible = nudTo.Visible = !single;
        foreach (var control in new Control[] { nudColumns, nudCellWidth, nudCellHeight, nudMarginLeft, nudMarginTop, nudGapX, nudGapY, nudFirstCode })
            control.Enabled = !single;

        cbFamily.Enabled = rbInstalled.Checked;
        bFontFile.Enabled = rbFontFile.Checked;
        nudFontThreshold.Enabled = cbRender.SelectedIndex == 0;

        var newFont = rbNewFont.Checked;
        tbNewName.Enabled = nudNewWidth.Enabled = nudNewHeight.Enabled = cbNewProportional.Enabled = newFont;
        cbTargetFont.Enabled = cbOnlyEmpty.Enabled = !newFont;
    }

    private void ScheduleUpdate()
    {
        if (_loading)
            return;
        timerUpdate.Stop();
        timerUpdate.Start();
    }

    private void timerUpdate_Tick(object? sender, EventArgs e)
    {
        timerUpdate.Stop();
        UpdateResult();
    }

    /// <summary>
    /// Znaky zo zdroja, pismo s nimi a nahlad (zoznam znakov a ukazka na tabuli).
    /// </summary>
    private void UpdateResult()
    {
        timerUpdate.Stop();
        UpdateModeControls();
        var target = Target();
        if (target is null)
            return;

        // novy ciel s inou vyskou - velkost pisma Windows sa prisposobi
        if (_source == ImportSource.Font && TargetHeight != _lastHeight)
            ApplyFontDefaults();

        var layout = GlyphLayoutFor(target);
        var from = (int)nudFrom.Value;
        var to = _source == ImportSource.Image && rbSingle.Checked ? from : Math.Max(from, (int)nudTo.Value);
        var notes = new Dictionary<int, string>();
        var glyphs = _source == ImportSource.Font ? FontGlyphs(layout, from, to, notes) : ImageGlyphs(layout, from, to, notes);

        Result = SymbolImport.Build(_file, target, glyphs, out _imported);
        foreach (var glyph in glyphs.Where(glyph => !_imported.Contains(glyph.Code)))
            notes[glyph.Code] = Resources.Import_NoteNotEmpty;

        FillGrid(target, from, to, notes);
        UpdateSample();

        var summary = string.Format(CultureInfo.CurrentCulture, Resources.Import_Summary, _imported.Count);
        var notInFont = notes.Count(note => note.Value == Resources.Import_NoteNotInFont);
        if (notInFont > 0)
            summary += " " + string.Format(CultureInfo.CurrentCulture, Resources.Import_SummaryNotInFont, notInFont);
        var notEmpty = notes.Count(note => note.Value == Resources.Import_NoteNotEmpty);
        if (notEmpty > 0)
            summary += " " + string.Format(CultureInfo.CurrentCulture, Resources.Import_SummaryNotEmpty, notEmpty);
        lSummary.Text = summary;
        lRangeInfo.Text = string.Format(CultureInfo.CurrentCulture, Resources.Import_RangeInfo, to - from + 1,
            _charset == SymbolCharset.Kamenicky ? Resources.Import_CharsetKamenicky : Resources.Import_CharsetWin1250);
        bImport.Enabled = _imported.Count > 0;
    }

    private List<ImportGlyph> FontGlyphs(GlyphLayout layout, int from, int to, Dictionary<int, string> notes)
    {
        var glyphs = new List<ImportGlyph>();
        if (CurrentFont is not { } font)
            return glyphs;

        var options = new TrueTypeOptions(font.AvailableStyle(SelectedStyle()), (float)nudSize.Value, (int)nudBaseline.Value,
            cbRender.SelectedIndex == 0, (int)nudFontThreshold.Value);
        using var rasterizer = new TrueTypeRasterizer(font.Family, options, layout.Height);
        for (var code = from; code <= to; code++)
        {
            // znak kodu podla kodovania stlpca Znak; riadiace kody znak nemaju
            if (SymbolChars.Text(code, _charset) is not [var c])
            {
                notes[code] = Resources.Import_NoteNoChar;
                continue;
            }

            if (rasterizer.Render(c) is { } drawn)
                glyphs.Add(layout.Fit(code, drawn.Ink, drawn.Advance));
            else
                notes[code] = Resources.Import_NoteNotInFont;
        }

        return glyphs;
    }

    private List<ImportGlyph> ImageGlyphs(GlyphLayout layout, int from, int to, Dictionary<int, string> notes)
    {
        var glyphs = new List<ImportGlyph>();
        if (_image is null)
            return glyphs;

        var single = rbSingle.Checked;
        var grid = single
            ? ImageGrid.Single(_image.Size, (int)nudScale.Value)
            : new ImageGrid((int)nudScale.Value, (int)nudCellWidth.Value, (int)nudCellHeight.Value, (int)nudColumns.Value,
                (int)nudMarginLeft.Value, (int)nudMarginTop.Value, (int)nudGapX.Value, (int)nudGapY.Value);
        var cells = grid.CellCount(_image.Size);
        var coverage = PixelCoverage.FromImage(_image, cbDarkInk.Checked);
        for (var code = from; code <= to; code++)
        {
            var index = single ? 0 : code - (int)nudFirstCode.Value;
            if (index < 0 || index >= cells)
            {
                notes[code] = Resources.Import_NoteOutside;
                continue;
            }

            var dots = grid.Read(coverage, index, (int)nudImageThreshold.Value);
            glyphs.Add(layout.Fit(code, dots, dots.GetLength(1)));
        }

        return glyphs;
    }

    private void FillGrid(ImportTarget target, int from, int to, Dictionary<int, string> notes)
    {
        var old = _images.ToList();
        _images.Clear();
        var original = target.IsNewFont ? null : _file.Fonts[target.FontIndex];
        var rows = new List<ResultRow>();
        for (var code = from; code <= to; code++)
        {
            var imported = _imported.Contains(code);
            var symbol = Result!.Symbols[code];
            rows.Add(new ResultRow(code, SymbolChars.Text(code, _charset), SymbolImage(imported ? symbol : null), symbol.Width,
                SymbolImage(original?.Symbols[code]), notes.GetValueOrDefault(code, "")));
        }

        dgvResult.Columns[4].Visible = original is not null;
        dgvResult.RowTemplate.Height = Math.Max(22, (Result!.Height * 2) + 4);
        dgvResult.DataSource = rows;
        foreach (var image in old)
            image.Dispose();
    }

    private Bitmap? SymbolImage(FontSymbol? symbol)
    {
        if (symbol is null)
            return null;
        var image = symbol.Image;
        _images.Add(image);
        return image;
    }

    private void UpdateSample()
    {
        if (Result is null)
            return;
        var raster = ElenText.Render(ElenBinParser.ToElenFont(Result), BoardPreview.Encode(tbSample.Text, _charset, new List<char>()));
        boardSample.BoardRows = raster.Height;
        boardSample.BoardColumns = raster.Width + 2;
        boardSample.Raster = raster;
    }

    private async void Option_Changed(object? sender, EventArgs e)
    {
        // prepnutie medzi nainstalovanym pismom a suborom - velkost podla noveho pisma
        if (!_loading && (sender == rbInstalled || sender == rbFontFile) && sender is RadioButton { Checked: true })
        {
            // bez vybraneho suboru ostane nainstalovane pismo
            if (rbFontFile.Checked && _fileFont is null && !await ChooseFontFile())
            {
                if (!IsDisposed && _fileFont is null)
                    rbInstalled.Checked = true;
                return;
            }
            ApplyFontDefaults();
        }

        ScheduleUpdate();
    }

    private void Font_Changed(object? sender, EventArgs e)
    {
        if (_loading)
            return;
        if (sender == cbFamily)
        {
            LoadInstalledFont();
            ApplyFontDefaults();
        }

        ScheduleUpdate();
    }

    private void NewHeight_Changed(object? sender, EventArgs e) => ScheduleUpdate();

    private void tbSample_TextChanged(object? sender, EventArgs e)
    {
        if (!_loading)
            UpdateSample();
    }

    private void bFontDefaults_Click(object? sender, EventArgs e)
    {
        ApplyFontDefaults();
        ScheduleUpdate();
    }

    private async void bFontFile_Click(object? sender, EventArgs e)
    {
        if (await ChooseFontFile())
        {
            ApplyFontDefaults();
            ScheduleUpdate();
        }
    }

    /// <summary>
    /// Dialog suboru na vlastnom vlakne - po zruseni okno hned reaguje, hoci Windows dialog este chvilu doupratuje.
    /// False = zruseny, okno zatvorene alebo predchadzajuci dialog este nedobehol.
    /// </summary>
    private async Task<bool> ShowFileDialog(OpenFileDialog dialog)
    {
        if (_choosingFile)
            return false;

        _choosingFile = true;
        try
        {
            return await dialog.ShowDialogAsync(this) == DialogResult.OK && !IsDisposed;
        }
        finally
        {
            _choosingFile = false;
        }
    }

    /// <summary>Vyber suboru pisma; pri zruseni alebo chybe ostane povodne pismo.</summary>
    private async Task<bool> ChooseFontFile()
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = Resources.Import_FontFilter;
        if (!await ShowFileDialog(dialog))
            return false;

        try
        {
            var font = TrueTypeSource.FromFile(dialog.FileName);
            _fileFont?.Dispose();
            _fileFont = font;
        }
        catch (Exception ex) when (ex is FormatException or IOException)
        {
            Log.Warning(ex, "Pismo {Path} na import sa nepodarilo nacitat", dialog.FileName);
            Utils.ShowError(string.Format(CultureInfo.CurrentCulture, Resources.Import_FontFileFailed, dialog.FileName));
            return false;
        }

        tbFontFile.Text = dialog.FileName;
        return true;
    }

    private async void bImageFile_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = Resources.Import_ImageFilter;
        if (!await ShowFileDialog(dialog))
            return;

        Bitmap image;
        try
        {
            // kopia - subor obrazka ostane volny
            using var loaded = System.Drawing.Image.FromFile(dialog.FileName);
            image = new Bitmap(loaded);
        }
        catch (Exception ex) when (ex is OutOfMemoryException or IOException or ArgumentException)
        {
            Log.Warning(ex, "Obrazok {Path} na import sa nepodarilo nacitat", dialog.FileName);
            Utils.ShowError(string.Format(CultureInfo.CurrentCulture, Resources.Import_ImageFailed, dialog.FileName, ex.Message));
            return;
        }

        _image?.Dispose();
        _image = image;
        _imagePath = dialog.FileName;
        tbImageFile.Text = dialog.FileName;
        GuessGrid(image);
        ScheduleUpdate();
    }

    /// <summary>
    /// Odhad mriezky noveho obrazka: 16 stlpcov, bunky podla sirky; 16 riadkov = kody od 0, 14 = od medzery.
    /// Tmava ci svetla kresba podla pozadia.
    /// </summary>
    private void GuessGrid(Bitmap image)
    {
        _loading = true;
        try
        {
            cbDarkInk.Checked = PixelCoverage.HasDarkInk(image);
            const int columns = 16;
            nudColumns.Value = columns;
            var cellWidth = Math.Max(1, image.Width / columns);
            nudCellWidth.Value = Math.Min(cellWidth, (int)nudCellWidth.Maximum);
            var rows = image.Height % 16 == 0 ? 16 : image.Height % 14 == 0 ? 14 : Math.Max(1, image.Height / cellWidth);
            nudCellHeight.Value = Math.Clamp(image.Height / rows, 1, (int)nudCellHeight.Maximum);
            nudFirstCode.Value = rows == 14 ? 32 : 0;
            foreach (var nud in new[] { nudMarginLeft, nudMarginTop, nudGapX, nudGapY })
                nud.Value = 0;
        }
        finally
        {
            _loading = false;
        }
    }

    private void bImport_Click(object? sender, EventArgs e)
    {
        if (_source == ImportSource.Image && _image is null)
        {
            Utils.ShowError(Resources.Import_NoImage);
            return;
        }

        if (rbNewFont.Checked && FontRules.NameError(tbNewName.Text, _file.Fonts.Select(font => font.Name)) is { } error)
        {
            Utils.ShowError(error);
            return;
        }

        // cakajuca zmena volieb sa este prejavi
        UpdateResult();
        if (Result is null || _imported.Count == 0)
        {
            Utils.ShowError(Resources.Import_Nothing);
            return;
        }

        DialogResult = DialogResult.OK;
    }

    /// <inheritdoc />
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        dgvResult.DataSource = null;
        foreach (var image in _images)
            image.Dispose();
        _images.Clear();
        _installedFont?.Dispose();
        _fileFont?.Dispose();
        _image?.Dispose();
    }

    /// <summary>Riadok zoznamu vysledku.</summary>
    internal sealed record ResultRow(int Code, string Char, Bitmap? New, int Width, Bitmap? Old, string Note);
}
