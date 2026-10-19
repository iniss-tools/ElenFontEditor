using System.ComponentModel;
using System.Globalization;
using ElenFontEditor.Entities;
using ExControls;
using ToolsCore.Iniss.Elen;
using ToolsCore.Tools;

namespace ElenFontEditor.Forms;

public partial class FFontManager : Form
{
    /// <summary>Nazov noveho pisma, doplni sa cislo - zapisuje sa do suboru, preto ASCII.</summary>
    private const string NewFontName = "PISMO_";

    /// <summary>Nove pismo je neproporcionalne 6 × 10 bodov ako prve pismo beznych suborov.</summary>
    private const int NewFontWidth = 6;

    private const int NewFontHeight = 10;

    private readonly FontsFile _file;
    private readonly ExBindingList<TableFont> _fonts;

    /// <summary>Kopia v mriezke → pismo v subore, z ktoreho sa pri ulozeni prenesu znaky.</summary>
    private readonly Dictionary<TableFont, TableFont> _originals = new(ReferenceEqualityComparer.Instance);

    public FFontManager(FontsFile file)
    {
        InitializeComponent();
        this.ApplyThemeAndFonts();

        _file = file;
        // kopie - Storno subor nezmeni a krok spat zachyti len potvrdene zmeny
        var copies = new List<TableFont>(file.Fonts.Count);
        foreach (var font in file.Fonts)
        {
            var copy = font.Clone();
            _originals.Add(copy, font);
            copies.Add(copy);
        }

        _fonts = new ExBindingList<TableFont>(copies);
        _fonts.AddingNew += Fonts_AddingNew;
        dgvFonts.DataSource = _fonts;
        UpdateAllowAddRows();
    }

    /// <summary>
    /// Pisma po ulozeni. Upravene pisma su nove objekty so znakmi prenesenymi z povodneho pisma, nove pisma maju
    /// prazdne znaky.
    /// </summary>
    internal static List<TableFont> CreateFonts(IEnumerable<TableFont> edited, IReadOnlyDictionary<TableFont, TableFont> originals) =>
        edited.Select(font => originals.TryGetValue(font, out var original)
                ? original.Clone(font.Name, font.MaxWidth, font.Height, font.IsProportional)
                : TableFont.CreateEmpty(font.Name, font.MaxWidth, font.Height, font.IsProportional))
            .ToList();

    /// <summary>Nove pismo pre novy riadok mriezky - s platnym nazvom, ktory este v subore nie je.</summary>
    internal static TableFont CreateNewFont(IEnumerable<TableFont> fonts)
    {
        var names = fonts.Select(font => font.Name).ToHashSet(StringComparer.Ordinal);
        var number = 1;
        while (names.Contains(NewFontName + number.ToString(CultureInfo.InvariantCulture)))
            number++;
        return TableFont.CreateEmpty(NewFontName + number.ToString(CultureInfo.InvariantCulture), NewFontWidth, NewFontHeight, false);
    }

    private void Fonts_AddingNew(object? sender, AddingNewEventArgs e)
    {
        e.NewObject = CreateNewFont(_fonts);
    }

    /// <summary>Novy riadok len kym ma subor menej ako 16 pisiem (riadok na pridanie sa do poctu nerata).</summary>
    private void UpdateAllowAddRows()
    {
        var allow = _fonts.Count < ElenFontFile.MaxFonts;
        if (dgvFonts.AllowUserToAddRows != allow)
            dgvFonts.AllowUserToAddRows = allow;
    }

    private void dgvFonts_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        UpdateAllowAddRows();
    }

    private void dgvFonts_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        UpdateAllowAddRows();
    }

    private void bSave_Click(object sender, EventArgs e)
    {
        if (!Validate())
            return;

        if (FontRules.FindProblem(_fonts) is { } problem)
        {
            Utils.ShowError(problem);
            return;
        }

        var fonts = CreateFonts(_fonts, _originals);
        _file.Fonts.Clear();
        _file.Fonts.AddRange(fonts);
        DialogResult = DialogResult.OK;
    }

    private void bStorno_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void FFontManager_FormClosing(object sender, FormClosingEventArgs e)
    {
        // chybna bunka nebrani zatvoreniu bez ulozenia
        if (DialogResult != DialogResult.OK)
            e.Cancel = false;
    }

    private void dgvFonts_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        var row = dgvFonts.Rows[e.RowIndex];
        // riadok na pridanie, do ktoreho sa nic nezapisalo
        if (row.IsNewRow)
            return;

        var text = e.FormattedValue?.ToString();
        string? error = null;
        if (e.ColumnIndex == dgveName.Index)
            error = FontRules.NameError(text, _fonts.Where((_, index) => index != e.RowIndex).Select(font => font.Name));
        else if (e.ColumnIndex == dgveMaxWidth.Index)
            error = FontRules.SizeError(text, TableFont.MaxWidthLimit);
        else if (e.ColumnIndex == dgveHeight.Index)
            error = FontRules.SizeError(text, TableFont.HeightLimit);

        row.ErrorText = error ?? "";
        e.Cancel = error != null;
    }

    private void dgvFonts_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
    {
        // subor medzery na konci nazvu nezachova
        if (e.ColumnIndex != dgveName.Index || e.Value is not string text)
            return;

        e.Value = text.Trim();
        e.ParsingApplied = true;
    }

    private void dgvFonts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        dgvFonts.Rows[e.RowIndex].ErrorText = "";
    }
}
