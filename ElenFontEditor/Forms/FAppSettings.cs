using ElenFontEditor.Entities;
using ElenFontEditor.Properties;
using ElenFontEditor.XML;
using ExControls;
using ToolsCore.Commands;
using ToolsCore.Forms;
using ToolsCore.Iniss.Tools;
using ToolsCore.XML;

namespace ElenFontEditor.Forms;

public partial class FAppSettings : FAppSettingsBase
{
    public new ElenFontEditorConfig Config => (ElenFontEditorConfig)base.Config;
    public new Styles<ElenFontEditorStyle> Styles => (Styles<ElenFontEditorStyle>)base.Styles;

    protected override IList<DesktopColumn> DefaultColumns => new DesktopColumns().GetValues();

    /// <inheritdoc />
    protected override string ReopenLastText => Resources.Settings_ReopenLast;

    /// <inheritdoc />
    protected override string ColumnsPageText => Resources.Settings_ColumnsPage;

    /// <param name="config">nastavenia programu</param>
    /// <param name="styles">styly programu</param>
    /// <param name="commands">prikazy hlavneho okna - strom skratiek podla jeho ponuky; null = podla katalogu</param>
    public FAppSettings(ElenFontEditorConfig config, Styles<ElenFontEditorStyle> styles, IEnumerable<AppCommand>? commands = null)
        : base(config, new Styles<ElenFontEditorStyle>(styles), GlobData.UsingStyle, typeof(ElenFontEditorStyle))
    {
        InitializeComponent();

        SetCommands(EfeCommands.All, commands);
        Columns = new ExBindingList<DesktopColumn>(Config.DesktopCols.GetValues());
        dgvColumns.DataSource = Columns;
    }

    /// <inheritdoc />
    protected override void OnLoad()
    {
        base.OnLoad();
        cboxStrictMode.Checked = Config.StrictMode;
        cbCharEncoding.BindEnum(new Dictionary<SymbolCharset, string>
        {
            [SymbolCharset.Win1250] = Resources.Settings_CharEncoding_Win1250,
            [SymbolCharset.Kamenicky] = Resources.Settings_CharEncoding_Kamenicky
        });
        cbCharEncoding.SelectedValue = Config.CharEncoding;
    }

    /// <inheritdoc />
    protected override bool OnSaving()
    {
        Config.DesktopCols.SetValues(Columns);
        Config.StrictMode = cboxStrictMode.Checked;
        if (cbCharEncoding.SelectedValue is SymbolCharset charset)
            Config.CharEncoding = charset;
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

        Styles<ElenFontEditorStyle>.WriteData(PathUtils.CombinePath(configsDir, ToolsCore.FileConsts.FileStyles)!, GlobData.Styles);
        XmlSerialization.WriteData(PathUtils.CombinePath(configsDir, ToolsCore.FileConsts.FileConfig)!, GlobData.Config);
    }

    /// <inheritdoc />
    protected override Style OnResetStyle(bool darkMode) => Styles<ElenFontEditorStyle>.GetDefaultStyle(darkMode);

    /// <inheritdoc />
    protected override Style CreateStyleInstance(string name) => new ElenFontEditorStyle {Name = name};
}