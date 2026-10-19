using ElenFontEditor.Entities;
using ElenFontEditor.Properties;
using ToolsCore.Tools;

namespace ElenFontEditor.Forms;

public partial class FRenameFile : Form
{
    private readonly FontsFile _file;
    private readonly string? _diskPath;

    /// <param name="file">otvoreny subor pisiem</param>
    /// <param name="diskPath">subor na disku, ktory otvoreny subor pri ulozeni nahradi (null = este neulozeny subor)</param>
    public FRenameFile(FontsFile file, string? diskPath)
    {
        InitializeComponent();
        this.ApplyThemeAndFonts();

        tbOldName.Text = file.Name;
        tbNewName.Text = file.Name;
        _file = file;
        _diskPath = diskPath;
    }

    /// <summary>
    /// Chyba noveho nazvu suboru alebo null. Subor s novym nazvom nesmie v priecinku byt - ulozenie by ho prepisalo;
    /// vynimkou je subor <paramref name="diskPath" />, ktory otvoreny subor aj tak nahradi (premenovanie spat).
    /// </summary>
    internal static string? NameError(FontsFile file, string newName, string? diskPath)
    {
        if (string.IsNullOrEmpty(newName))
            return Resources.RenameFile_NameEmpty;
        if (file.Name == newName)
            return Resources.RenameFile_NameSame;
        if (newName.Length > FontsFile.NameMaxLength)
            return Resources.FMain_FileName_Length;
        if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            return Resources.RenameFile_InvalidChars;
        if (NewPath(file, newName) is not { } path)
            return Resources.RenameFile_InvalidPath;
        if (Path.Exists(path) && !(diskPath is not null && SamePath(path, diskPath)))
            return Resources.FMain_FileExists;
        return null;
    }

    /// <summary>Cesta suboru s novym nazvom v tom istom priecinku a s tou istou priponou alebo null.</summary>
    internal static string? NewPath(FontsFile file, string newName)
    {
        var dir = Path.GetDirectoryName(file.PathName);
        return dir is null ? null : Path.Combine(dir, newName + Path.GetExtension(file.PathName));
    }

    private static bool SamePath(string path, string other) =>
        string.Equals(Path.GetFullPath(path), Path.GetFullPath(other), StringComparison.OrdinalIgnoreCase);

    private void bSave_Click(object sender, EventArgs e)
    {
        var newName = tbNewName.Text;
        if (NameError(_file, newName, _diskPath) is { } error)
        {
            Utils.ShowError(error);
            DialogResult = DialogResult.None;
            return;
        }

        _file.Name = newName;
        _file.PathName = NewPath(_file, newName)!;
        DialogResult = DialogResult.OK;
    }

    private void bStorno_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}
