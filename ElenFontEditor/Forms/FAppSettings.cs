using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ElenFontEditor.XML;
using ExControls;
using ToolsCore;
using ToolsCore.Forms;
using ToolsCore.Tools;
using ToolsCore.XML;

namespace ElenFontEditor.Forms;

public partial class FAppSettings : FAppSettingsBase
{
    public new ElenFontEditorConfig Config => base.Config as ElenFontEditorConfig;
    public new Styles<ElenFontEditorStyle> Styles => base.Styles as Styles<ElenFontEditorStyle>;

    protected override IList<CmdShortcut> DefaultShortcuts => new AppShortcuts().GetValues();
    protected override IList<DesktopColumn> DefaultColumns => new DesktopColumns().GetValues();

    public FAppSettings(ElenFontEditorConfig config, Styles<ElenFontEditorStyle> styles) 
        : base(config, new Styles<ElenFontEditorStyle>(styles), GlobData.UsingStyle, typeof(ElenFontEditorStyle))
    {
        InitializeComponent();

        Shortcuts = new ExBindingList<CmdShortcut>(Config.Shortcuts.GetValues());
        Columns = new ExBindingList<DesktopColumn>(Config.DesktopCols.GetValues());

        dgvShortcuts.DataSource = Shortcuts;
        dgvColumns.DataSource = Columns;
    }

    /// <inheritdoc />
    protected override void OnLoad()
    {
        base.OnLoad();
        cboxStrictMode.Checked = Config.StrictMode;
    }

    /// <inheritdoc />
    protected override bool OnSaving()
    {
        base.OnSaving();

        Config.Shortcuts.SetValues(Shortcuts);
        Config.DesktopCols.SetValues(Columns);
        Config.StrictMode = cboxStrictMode.Checked;
        return true;
    }

    /// <inheritdoc />
    protected override void SaveData()
    {
        GlobData.Config = Config;
        GlobData.UsingStyle = UsingStyle as ElenFontEditorStyle;
        GlobSettings.UsingStyle = UsingStyle;
        GlobData.Styles = Styles;
        
        var configsDir = Utils.CombinePath(Application.StartupPath, ToolsCore.FileConsts.CONFIG_PATH);
        if (!Directory.Exists(configsDir))
            Directory.CreateDirectory(configsDir);

        Styles<ElenFontEditorStyle>.WriteData(Utils.CombinePath(configsDir, ToolsCore.FileConsts.FILE_STYLES), GlobData.Styles);
        XmlSerialization.WriteData(Utils.CombinePath(configsDir, ToolsCore.FileConsts.FILE_CONFIG), GlobData.Config);
    }

    /// <inheritdoc />
    protected override Style OnResetStyle(bool darkMode) => Styles<ElenFontEditorStyle>.GetDefaultStyle(darkMode);

    /// <inheritdoc />
    protected override Style CreateStyleInstance(string name) => new ElenFontEditorStyle {Name = name};
}