using System.Reflection;
using System.Windows.Forms;
using ElenFontEditor.Controls;
using ElenFontEditor.Forms;
using ElenFontEditor.XML;
using ExControls;
using ToolsCore;
using ToolsCore.Commands;
using ToolsCore.XML;

namespace ElenFontEditor.Tests;

/// <summary>
/// Ukotvitelne panely hlavneho okna bez zobrazenia: zoznam znakov vlavo, editor znaku v dokumente, obnovenie rozlozenia
/// z nastaveni a navrat k predvolenemu.
/// </summary>
[TestClass]
public sealed class DockingFormTests
{
    private const BindingFlags Any = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    // konstruktor okna config.xml nezapisuje (stlpce tabulky sa ukladaju az po zmene obsluhou), zatvorenie by ho zapisalo -
    // okna sa preto len zahodia
    private static FMain MainWindow(string? layout = null)
    {
        var styles = Styles<ElenFontEditorStyle>.CreateDefault();
        var session = new AppSession<ElenFontEditorConfig, ElenFontEditorStyle>(new ElenFontEditorConfig { DockLayout = layout }, styles,
            styles.StyleList[0]);
        // inicializator vlastnosti obide setter - tema okien cita styl z GlobSettings
        session.UsingStyle = styles.StyleList[0];
        GlobData.Session = session;
        var form = new FMain();
        form.CreateControl();
        return form;
    }

    private static T Field<T>(Control control, string name) => (T)control.GetType().GetField(name, Any)!.GetValue(control)!;

    private static ExDockManager Dock(FMain form) => Field<ExDockManager>(form, "_dock");

    private static ExDockPanel Panel(FMain form, string key) => Dock(form).Panels.Single(panel => panel.Key == key);

    [STATestMethod]
    public void HlavneOkno_ZoznamZnakovVPaneliAEditorVDokumente()
    {
        using var form = MainWindow();

        CollectionAssert.AreEqual(new[] { "Symbols" }, Dock(form).Panels.Select(panel => panel.Key).ToArray());
        Assert.AreSame(Panel(form, "Symbols"), Field<DataGridView>(form, "dgvSymbols").Parent);
        Assert.AreSame(Dock(form).Document, Field<SymbolEditor>(form, "symbolEditor").Parent);
        Assert.AreEqual(ExDockSide.Left, Panel(form, "Symbols").Side);
        Assert.IsTrue(Panel(form, "Symbols").IsShown);
        Assert.IsTrue(Field<SplitContainer>(form, "splitContainer1").IsDisposed, "deliaci list navrhu sa zahodi");
    }

    [STATestMethod]
    public void Rozlozenie_SkrytyZoznamZnakovSaObnoviAPrikazVratiPredvolene()
    {
        string layout;
        using (var first = MainWindow())
        {
            Dock(first).HidePanel(Panel(first, "Symbols"));
            layout = Dock(first).SaveLayout();
        }

        using var form = MainWindow(layout);
        Assert.IsFalse(Panel(form, "Symbols").IsShown);

        Field<CommandSet>(form, "_commands")["ShowSymbols"].Execute();
        Assert.IsTrue(Panel(form, "Symbols").IsShown);

        Dock(form).HidePanel(Panel(form, "Symbols"));
        Field<CommandSet>(form, "_commands")["ResetLayout"].Execute();
        Assert.IsTrue(Panel(form, "Symbols").IsShown);
    }

    [STATestMethod]
    public void Rozlozenie_NeplatneOstavaPredvolene()
    {
        using var form = MainWindow("<nie je rozlozenie />");

        Assert.IsTrue(Panel(form, "Symbols").IsShown);
        Assert.AreEqual(ExDockSide.Left, Panel(form, "Symbols").Side);
    }
}
