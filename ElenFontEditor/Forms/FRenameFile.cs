using System.IO;
using System.Windows.Forms;
using ElenFontEditor.Entities;
using ToolsCore.Tools;

namespace ElenFontEditor.Forms;

public partial class FRenameFile : Form
{
    private readonly FontsFile _file;
    public FRenameFile(FontsFile file)
    {
        InitializeComponent();
        this.ApplyThemeAndFonts();

        tbOldName.Text = file.Name;
        tbNewName.Text = file.Name;
        _file = file;
    }

    private void bSave_Click(object sender, System.EventArgs e)
    {
        var newName = tbNewName.Text;
        if (string.IsNullOrEmpty(newName))
        {
            Utils.ShowError(@"Názov súboru nemôže byť prázdny.");
            DialogResult = DialogResult.None;
            return;
        }

        if (_file.Name == newName)
        {
            Utils.ShowError(@"Nový názov súboru je rovnaký ako pôvodný.");
            DialogResult = DialogResult.None;
            return;
        }

        if (newName.Length > 32)
        {
            Utils.ShowError(@"Názov súboru musí byť kratší ako 32 znakov.");
            DialogResult = DialogResult.None;
            return;
        }

        var dir = Path.GetDirectoryName(_file.PathName);
        if (dir == null)
        {
            Utils.ShowError(@"Neplatná cesta k súboru.");
            DialogResult = DialogResult.None;
            return;
        }

        var ext = Path.GetExtension(_file.PathName);
        var pathNew =  Path.Combine(dir, newName + ext);

        _file.Name = newName;
        _file.PathName = pathNew;
        DialogResult = DialogResult.OK;
    }

    private void bStorno_Click(object sender, System.EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}