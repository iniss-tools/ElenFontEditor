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

    #region Properties

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Index")]
    public DesktopColumn Index
    {
        get => field ??= InitColumn(nameof(Index));
        set
        {
            field = value;
            AssignColumnProps(ref field, nameof(Index));
        }
    } = InitColumn(nameof(Index));

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Char")]
    public DesktopColumn Char
    {
        get => field ??= InitColumn(nameof(Char));
        set
        {
            field = value;
            AssignColumnProps(ref field, nameof(Char));
        }
    } = InitColumn(nameof(Char));

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Symbol")]
    public DesktopColumn Symbol
    {
        get => field ??= InitColumn(nameof(Symbol));
        set
        {
            field = value;
            AssignColumnProps(ref field, nameof(Symbol));
        }
    } = InitColumn(nameof(Symbol));

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("Width")]
    public DesktopColumn Width
    {
        get => field ??= InitColumn(nameof(Width));
        set
        {
            field = value;
            AssignColumnProps(ref field, nameof(Width));
        }
    } = InitColumn(nameof(Width));

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("RowCount")]
    public DesktopColumn RowCount
    {
        get => field ??= InitColumn(nameof(RowCount));
        set
        {
            field = value;
            AssignColumnProps(ref field, nameof(RowCount));
        }
    } = InitColumn(nameof(RowCount));

    /// <summary>
    ///     
    /// </summary>
    [XmlElement("ColCount")]
    public DesktopColumn ColCount
    {
        get => field ??= InitColumn(nameof(ColCount));
        set
        {
            field = value;
            AssignColumnProps(ref field, nameof(ColCount));
        }
    } = InitColumn(nameof(ColCount));

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

    private static void AssignColumnProps(ref DesktopColumn? obj, string propname)
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
    
    // kopia stlpca uz ma nazov a priznaky z Props (priradil ich setter povodnej instancie) - staci kopirovat polia;
    // inicializatory poli sa v kopirovacom konstruktore recordu nespustaju, preto sa kopiruje vsetko
    protected DesktopColumns(DesktopColumns original)
    {
        if (original.Index != null) Index = original.Index with { };
        if (original.Char != null) Char = original.Char with { };
        if (original.Symbol != null) Symbol = original.Symbol with { };
        if (original.Width != null) Width = original.Width with { };
        if (original.RowCount != null) RowCount = original.RowCount with { };
        if (original.ColCount != null) ColCount = original.ColCount with { };
    }
}