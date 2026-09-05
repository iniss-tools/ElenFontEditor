using ElenFontEditor.Entities;
using ExControls;
using ToolsCore.Tools;

namespace ElenFontEditor.Forms;

public partial class FFontManager : Form
{
    private readonly FontsFile _file;
    private readonly ExBindingList<TableFont> _fonts;

    public FFontManager(FontsFile file)
    {
        InitializeComponent();
        this.ApplyThemeAndFonts();

        _file = file;
        _fonts = new ExBindingList<TableFont>(file.Fonts);
        dgvFonts.DataSource = _fonts;
    }

    private void dgvFonts_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        if (dgvFonts.Rows.Count >= 16) 
            dgvFonts.AllowUserToAddRows = false;
    }

    private void dgvFonts_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        if (dgvFonts.Rows.Count < 16) 
            dgvFonts.AllowUserToAddRows = true;
    }

    private void bSave_Click(object sender, System.EventArgs e)
    {
        var fontsCopy = new List<TableFont>(_fonts.Count);
        fontsCopy.AddRange(_fonts.Select(font => font.Clone(font.MaxWidth, font.Height)));

        _file.Fonts.Clear();
        _file.Fonts.AddRange(fontsCopy);
        DialogResult = DialogResult.OK;
    }

    private void bStorno_Click(object sender, System.EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void dgvFonts_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (e.ColumnIndex != 1 && e.ColumnIndex != 2)
            return;

        if (!int.TryParse(e.FormattedValue.ToString(), out var val))
        {
            e.Cancel = true;
            return;
        }

        switch (e.ColumnIndex)
        {
            case 1 when val is < 0 or > 24:
                e.Cancel = true;
                return;
            case 2 when val is < 0 or > 20:
                e.Cancel = true;
                return;
        }
    }
}