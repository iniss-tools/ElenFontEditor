using ElenFontEditor.Properties;
using ExControls;
using ToolsCore.Tools;

namespace ElenFontEditor.Forms;

// ukotvitelne panely (ExDockManager): editor znaku v dokumente, zoznam znakov vlavo
public partial class FMain
{
    // kluc panela v ulozenom rozlozeni (config.xml) - nemenit
    private const string SymbolsPanelKey = "Symbols";

    private readonly ExDockManager _dock = AppDock.Create();
    private readonly ExDockPanel _symbolsPanel = new(SymbolsPanelKey, Resources.Panel_Symbols);
    private string _defaultLayout = "";

    /// <summary>
    /// Navrh okna ma zoznam znakov a editor v deliacom liste. Za behu ide zoznam do ukotvitelneho panela, editor do
    /// dokumentu a deliaci list sa zahodi; rozlozenie z nastaveni sa obnovi az po teme a pismach.
    /// </summary>
    private void InitDocking()
    {
        SuspendLayout();
        var designed = splitContainer1;
        dgvSymbols.Parent?.Controls.Remove(dgvSymbols);
        symbolEditor.Parent?.Controls.Remove(symbolEditor);
        dgvSymbols.Dock = DockStyle.Fill;
        _symbolsPanel.Controls.Add(dgvSymbols);
        symbolEditor.Dock = DockStyle.Fill;
        _dock.Document.Controls.Add(symbolEditor);

        // na miesto deliaceho listu - pred panelmi ukotvenymi k okraju, aby vyplnil zvysok okna
        var index = Controls.GetChildIndex(designed);
        Controls.Remove(designed);
        designed.Dispose();
        Controls.Add(_dock);
        Controls.SetChildIndex(_dock, index);

        _dock.AddPanel(_symbolsPanel, ExDockSide.Left);
        // sirka zony je v logickych bodoch (96 DPI) - spravca ju prepocita sam
        _dock.SetZoneSize(ExDockSide.Left, 300);
        ResumeLayout(true);

        _defaultLayout = _dock.SaveLayout();
        // rozlozenie sa uklada pri zatvoreni okna spolu s ostatnymi nastaveniami (az ked zatvorenie nikto nezrusil)
        FormClosed += (_, _) =>
        {
            GlobData.Config.DockLayout = _dock.SaveLayout();
            SaveConfig();
        };
    }

    private void CreateViewCommands()
    {
        _commands.Add(EfeCommands.ShowSymbols, () => _dock.ShowPanel(_symbolsPanel)).Bind(tsmimShowSymbols);
        _commands.Add(EfeCommands.ResetLayout, () => _dock.LoadLayout(_defaultLayout)).Bind(tsmimResetLayout);
    }
}
