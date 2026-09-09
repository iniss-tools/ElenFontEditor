using System.Reflection;
using System.Xml.Serialization;
using ToolsCore.XML;

namespace ElenFontEditor.XML;

/// <summary>
///     Obsahuje zoznam všetkých možných klávesových skratiek pre program.
/// </summary>
public record AppShortcuts()
{
    private static readonly Type ClassType = typeof(AppShortcuts);

    [XmlIgnore]
    private static readonly Dictionary<string, (Shortcut shortcut, string name)> Props = new()
    {
        [nameof(New)] = (Shortcut.CtrlShiftS, "Nový"),
        [nameof(Open)] = (Shortcut.CtrlO, "Otvoriť"),
        [nameof(Save)] = (Shortcut.CtrlS, "Uložiť"),
        [nameof(FontManager)] = (Shortcut.F1, "Správa písiem"),
        [nameof(RenameFile)] = (Shortcut.F2, "Premenovať súbor"),
        [nameof(AppSettings)] = (Shortcut.CtrlShiftN, "Nastavenia programu")
    };

    #region Fields

    private CmdShortcut _new = InitShortcut(nameof(New));
    private CmdShortcut _open = InitShortcut(nameof(Open));
    private CmdShortcut _save = InitShortcut(nameof(Save));
    private CmdShortcut _fontManager = InitShortcut(nameof(FontManager));
    private CmdShortcut _renameFile = InitShortcut(nameof(RenameFile));
    private CmdShortcut _appSettings = InitShortcut(nameof(AppSettings));

    #endregion
    
    #region Properties

    /// <summary>
    ///     Skratka pre uloženie vsetkych jazykov banky.
    /// </summary>
    [XmlElement("New")]
    public CmdShortcut New
    {
        get => _new ??= InitShortcut(nameof(New));
        set
        {
            _new = value;
            AssignShortcutProps(ref _new, nameof(New));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Open")]
    public CmdShortcut Open
    {
        get => _open ??= InitShortcut(nameof(Open));
        set
        {
            _open = value;
            AssignShortcutProps(ref _open, nameof(Open));
        }
    }

    /// <summary>
    ///     Skratka pre uloženie jazyka banky.
    /// </summary>
    [XmlElement("Save")]
    public CmdShortcut Save
    {
        get => _save ??= InitShortcut(nameof(Save));
        set
        {
            _save = value;
            AssignShortcutProps(ref _save, nameof(Save));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("FontManager")]
    public CmdShortcut FontManager
    {
        get => _fontManager ??= InitShortcut(nameof(FontManager));
        set
        {
            _fontManager = value;
            AssignShortcutProps(ref _fontManager, nameof(FontManager));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("RenameFile")]
    public CmdShortcut RenameFile
    {
        get => _renameFile ??= InitShortcut(nameof(RenameFile));
        set
        {
            _renameFile = value;
            AssignShortcutProps(ref _renameFile, nameof(RenameFile));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("AppSettings")]
    public CmdShortcut AppSettings
    {
        get => _appSettings ??= InitShortcut(nameof(AppSettings));
        set
        {
            _appSettings = value;
            AssignShortcutProps(ref _appSettings, nameof(AppSettings));
        }
    }

    #endregion

    public IList<CmdShortcut> GetValues()
    {
        var properties = ClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        return properties.Select(prop => (CmdShortcut)prop.GetValue(this)!).ToList();
    }

    public void SetValues(IEnumerable<CmdShortcut> shortcuts)
    {
        foreach (var shortcut in shortcuts) 
            ClassType.GetProperty(shortcut.PropertyName)?.SetValue(this, shortcut);
    }

    private static CmdShortcut InitShortcut(string propname) => new(Props[propname].shortcut, Props[propname].name, propname);

    private static void AssignShortcutProps(ref CmdShortcut obj, string propname)
    {
        if (obj is null)
        {
            obj = InitShortcut(propname);
        }
        else
        {
            obj.Name = Props[propname].name;
            obj.PropertyName = propname;
        }
    }

    // Every property setter below unconditionally assigns its backing field before this constructor
    // exits (see the "set" accessors above), but Roslyn's per-constructor flow analysis doesn't credit
    // assignment performed indirectly through a property setter call - it only sees `this` escaping into
    // a method call and forgets the field's null-state. All backing fields are genuinely never null here.
#pragma warning disable CS8618
    protected AppShortcuts(AppShortcuts original)
    {
        Open = original.Open with { };
        Save = original.Save with { };
        New = original.New with { };
        FontManager = original.FontManager with { };
        RenameFile = original.RenameFile with { };
        AppSettings = original.AppSettings with { };
    }
#pragma warning restore CS8618
}