using System.Reflection;
using System.Xml.Serialization;
using ToolsCore.XML;

namespace ElenFontEditor.XML;

/// <summary>
///     Obsahuje zoznam všetkých možných stĺpcov pre tabuľku na pracovnej ploche programu
/// </summary>
public record DesktopColumns()
{
    private static readonly Type ClassType = typeof(DesktopColumns);

    [XmlIgnore]
    private static readonly Dictionary<string, (string name, int order, int minWidth, bool visible)> Props = new()
    {
        [nameof(Index)] = ("Index", 0, 50, true),
        [nameof(Char)] = ("Znak", 1, 60, true),
        [nameof(Symbol)] = ("Symbol", 2, 50, true),
        [nameof(Width)] = ("Šírka", 3, 50, true),
        [nameof(RowCount)] = ("Počet riadkov", 4, 50, true),
        [nameof(ColCount)] = ("Počet stĺpcov", 5, 50, true)
    };

    #region Fields

    private DesktopColumn _index = InitColumn(nameof(Index));
    private DesktopColumn _char = InitColumn(nameof(Char));
    private DesktopColumn _symbol = InitColumn(nameof(Symbol));
    private DesktopColumn _width = InitColumn(nameof(Width));
    private DesktopColumn _rowCount = InitColumn(nameof(RowCount));
    private DesktopColumn _colCount = InitColumn(nameof(ColCount));

    #endregion

    #region Properties

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Index")]
    public DesktopColumn Index
    {
        get => _index ??= InitColumn(nameof(Index));
        set
        {
            _index = value;
            AssignColumnProps(ref _index, nameof(Index));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Char")]
    public DesktopColumn Char
    {
        get => _char ??= InitColumn(nameof(Char));
        set
        {
            _char = value;
            AssignColumnProps(ref _char, nameof(Char));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Symbol")]
    public DesktopColumn Symbol
    {
        get => _symbol ??= InitColumn(nameof(Symbol));
        set
        {
            _symbol = value;
            AssignColumnProps(ref _symbol, nameof(Symbol));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Width")]
    public DesktopColumn Width
    {
        get => _width ??= InitColumn(nameof(Width));
        set
        {
            _width = value;
            AssignColumnProps(ref _width, nameof(Width));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("RowCount")]
    public DesktopColumn RowCount
    {
        get => _rowCount ??= InitColumn(nameof(RowCount));
        set
        {
            _rowCount = value;
            AssignColumnProps(ref _rowCount, nameof(RowCount));
        }
    }

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("ColCount")]
    public DesktopColumn ColCount
    {
        get => _colCount ??= InitColumn(nameof(ColCount));
        set
        {
            _colCount = value;
            AssignColumnProps(ref _colCount, nameof(ColCount));
        }
    }

    #endregion

    /// <summary>
    ///     Vráti zoradený zoznam všetkých možných stĺpcov pre tabuľku na pracovnej ploche programu
    /// </summary>
    /// <returns></returns>
    public IList<DesktopColumn> GetValues()
    {
        var properties = ClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        var ordered = properties.Select(prop => (DesktopColumn)prop.GetValue(this)!).ToList();
        return ordered.OrderBy(i => i.Order).ToList();
    }

    public void SetValues(IEnumerable<DesktopColumn> columns)
    {
        var index = 0;
        foreach (var column in columns)
        {
            column.Order = index++;
            ClassType.GetProperty(column.PropertyName)?.SetValue(this, column);
        }
    }

    private static DesktopColumn InitColumn(string propname)
        => new(Props[propname].name, propname, Props[propname].order, Props[propname].minWidth, Props[propname].visible);

    private static void AssignColumnProps(ref DesktopColumn obj, string propname)
    {
        if (obj is null)
        {
            obj = InitColumn(propname);
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
    protected DesktopColumns(DesktopColumns original)
    {
        Index = original.Index with { };
        Char = original.Char with { };
        Symbol = original.Symbol with { };
        Width = original.Width with { };
        RowCount = original.RowCount with { };
        ColCount = original.ColCount with { };
    }
#pragma warning restore CS8618
}