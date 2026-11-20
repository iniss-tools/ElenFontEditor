using System.Reflection;
using System.Windows.Forms;
using ElenFontEditor.Entities;
using ElenFontEditor.Forms;
using ElenFontEditor.XML;
using ExControls;
using ToolsCore;
using ToolsCore.Commands;
using ToolsCore.XML;

namespace ElenFontEditor.Tests;

/// <summary>
/// Kroky spat a znova v hlavnom okne: obnovene pisma sa zobrazia bez prepnutia vyberu pisma (pole Pismo nepreblikne).
/// </summary>
[TestClass]
public sealed class HistoryFormTests
{
    private const BindingFlags Any = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    private static T Field<T>(object target, string name) => (T)target.GetType().GetField(name, Any)!.GetValue(target)!;

    private static void Call(object target, string name, params object?[] args) =>
        target.GetType().GetMethod(name, Any, args.Select(a => a!.GetType()).ToArray())!.Invoke(target, args);

    // okno sa len zahodi - zatvorenie by zapisalo config.xml (pozri DockingFormTests)
    private static (FMain Form, FontsFile File) MainWindowWithFile()
    {
        var styles = Styles<ElenFontEditorStyle>.CreateDefault();
        var session = new AppSession<ElenFontEditorConfig, ElenFontEditorStyle>(new ElenFontEditorConfig(), styles, styles.StyleList[0]);
        session.UsingStyle = styles.StyleList[0];
        GlobData.Session = session;
        var form = new FMain();
        form.CreateControl();
        // krok spat sa zapamata az s vytvorenym handlom okna (MarkChanged)
        _ = form.Handle;

        var file = new FontsFile { Name = "TEST", PathName = "TEST.bin" };
        file.Fonts.Add(TableFont.CreateEmpty("PRVE", 6, 8, false));
        file.Fonts.Add(TableFont.CreateEmpty("DRUHE", 8, 8, true));
        form.GetType().GetField("_openedFile", Any)!.SetValue(form, file);
        Call(form, "OnOpenFile", file, false);
        return (form, file);
    }

    [STATestMethod]
    public void SpatAZnova_VyberPismaOstane_ZnakyObnovenehoPisma()
    {
        var (form, file) = MainWindowWithFile();
        using var _ = form;
        var fonts = Field<ExToolStripComboBox>(form, "tscomboFonts");
        var grid = Field<DataGridView>(form, "dgvSymbols");
        fonts.SelectedIndex = 1;

        // uprava sirky znaku v druhom pisme ako jeden krok
        file.Fonts[1].Symbols[65].Width = 3;
        Call(form, "MarkChanged", "sirka");
        Call(form, "FlushCommit");

        var selected = new List<int>();
        fonts.SelectedIndexChanged += (_, _) => selected.Add(fonts.SelectedIndex);
        var commands = Field<CommandSet>(form, "_commands");

        commands["Undo"].Execute();
        Assert.AreEqual(1, fonts.SelectedIndex);
        Assert.AreSame(file.Fonts[1].Symbols, grid.DataSource, "zoznam znakov ukazuje obnovene pismo");
        Assert.AreEqual(8, file.Fonts[1].Symbols[65].Width);

        commands["Redo"].Execute();
        Assert.AreEqual(1, fonts.SelectedIndex);
        Assert.AreSame(file.Fonts[1].Symbols, grid.DataSource);
        Assert.AreEqual(3, file.Fonts[1].Symbols[65].Width);

        CollectionAssert.DoesNotContain(selected, -1, "vyber pisma sa pri kroku spat neprepina cez prazdny");
    }
}
