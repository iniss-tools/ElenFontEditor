using ElenFontEditor.XML;
using ExControls;
using ToolsCore.Forms;
using ToolsCore.Tools;
using ToolsCore.XML;

namespace ElenFontEditor.Forms;

public partial class FAppSettings : FAppSettingsBase
{
    public new ElenFontEditorConfig Config => (ElenFontEditorConfig)base.Config;
    public new Styles<ElenFontEditorStyle> Styles => (Styles<ElenFontEditorStyle>)base.Styles;

    protected override IList<CmdShortcut> DefaultShortcuts => ShortcutMap.DefaultRows(EfeCommands.All);
    protected override IList<DesktopColumn> DefaultColumns => new DesktopColumns().GetValues();

    public FAppSettings(ElenFontEditorConfig config, Styles<ElenFontEditorStyle> styles) 
        : base(config, new Styles<ElenFontEditorStyle>(styles), GlobData.UsingStyle, typeof(ElenFontEditorStyle))
    {
        InitializeComponent();

        Shortcuts = new ExBindingList<CmdShortcut>(Config.Shortcuts.ToRows(EfeCommands.All));
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
        Config.Shortcuts.SetFromRows(Shortcuts);
        Config.DesktopCols.SetValues(Columns);
        Config.StrictMode = cboxStrictMode.Checked;
        return base.OnSaving();
    }

    /// <inheritdoc />
    protected override void SaveData()
    {
        GlobData.Config = Config;
        GlobData.UsingStyle = (ElenFontEditorStyle)UsingStyle;
        GlobData.Styles = Styles;
        
        var configsDir = ToolsCore.AppPaths.ConfigDir;
        if (!Directory.Exists(configsDir))
            Directory.CreateDirectory(configsDir);

        Styles<ElenFontEditorStyle>.WriteData(Utils.CombinePath(configsDir, ToolsCore.FileConsts.FILE_STYLES)!, GlobData.Styles);
        XmlSerialization.WriteData(Utils.CombinePath(configsDir, ToolsCore.FileConsts.FILE_CONFIG)!, GlobData.Config);
    }

    /// <inheritdoc />
    protected override Style OnResetStyle(bool darkMode) => Styles<ElenFontEditorStyle>.GetDefaultStyle(darkMode);

    /// <inheritdoc />
    protected override Style CreateStyleInstance(string name) => new ElenFontEditorStyle {Name = name};
}