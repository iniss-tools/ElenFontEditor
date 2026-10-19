using System.Collections;
using System.Globalization;
using System.Resources;
using ElenFontEditor.Controls;
using ElenFontEditor.Forms;

namespace ElenFontEditor.Tests;

/// <summary>
/// Preklad rozhrania do cestiny - kazdy text zo slovenskych zdrojov ma cesky preklad.
/// </summary>
[TestClass]
public class LocalizationTests
{
    private static readonly CultureInfo Czech = CultureInfo.GetCultureInfo("cs");

    /// <summary>Vlastnosti prvkov s textom pre obsluhu.</summary>
    private static readonly string[] TextProperties = [".Text", ".HeaderText", ".ToolTipText", ".HintText", ".Filter", ".Title"];

    /// <summary>Texty, ktore obsluha nevidi alebo ich program nastavi sam.</summary>
    private static readonly string[] NotTranslated =
        ["FMain:menuStripMain.Text", "FMain:toolStripMain.Text", "FMain:statusStripMain.Text", "FMain:tsslPosition.Text", "FMain:$this.Text"];

    public static IEnumerable<object[]> Forms =>
    [
        [typeof(FMain)], [typeof(FAppSettings)], [typeof(FFontManager)], [typeof(FRenameFile)], [typeof(SymbolEditor)]
    ];

    [TestMethod]
    [DynamicData(nameof(Forms))]
    public void Formular_KazdyTextMaCeskyPreklad(Type form)
    {
        var manager = new ResourceManager(form);
        var slovak = Strings(manager, CultureInfo.InvariantCulture)
            .Where(entry => TextProperties.Any(entry.Key.EndsWith) && !NotTranslated.Contains($"{form.Name}:{entry.Key}"))
            .ToList();
        var czech = Strings(manager, Czech);

        Assert.IsNotEmpty(slovak);
        CollectionAssert.AreEquivalent(slovak.Select(entry => entry.Key).ToList(), czech.Keys.ToList());
        foreach (var (key, value) in czech)
            Assert.IsFalse(string.IsNullOrWhiteSpace(value), key);
    }

    [TestMethod]
    public void Resources_KazdyTextMaCeskyPreklad()
    {
        var manager = new ResourceManager("ElenFontEditor.Properties.Resources", typeof(FMain).Assembly);
        var slovak = Strings(manager, CultureInfo.InvariantCulture);
        var czech = Strings(manager, Czech);

        CollectionAssert.AreEquivalent(slovak.Keys.ToList(), czech.Keys.ToList());
        foreach (var (key, value) in slovak)
            Assert.AreEqual(Placeholders(value), Placeholders(czech[key]), key);
    }

    private static Dictionary<string, string> Strings(ResourceManager manager, CultureInfo culture)
    {
        var set = manager.GetResourceSet(culture, createIfNotExists: true, tryParents: false);
        Assert.IsNotNull(set, culture.Name);
        return set.Cast<DictionaryEntry>()
            .Where(entry => entry.Value is string && !((string)entry.Key).StartsWith(">>", StringComparison.Ordinal))
            .ToDictionary(entry => (string)entry.Key, entry => (string)entry.Value!);
    }

    /// <summary>Zastupne znaky {0}, {1}... v poradi - preklad ich musi mat rovnake.</summary>
    private static string Placeholders(string text) =>
        string.Join(",", System.Text.RegularExpressions.Regex.Matches(text, @"\{\d+\}").Select(m => m.Value).Order(StringComparer.Ordinal));
}
