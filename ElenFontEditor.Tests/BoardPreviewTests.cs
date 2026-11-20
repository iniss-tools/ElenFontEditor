using System.Reflection;
using System.Windows.Forms;
using ElenFontEditor.Controls;
using ElenFontEditor.Entities;
using ElenFontEditor.Properties;
using ElenFontEditor.XML;

namespace ElenFontEditor.Tests;

/// <summary>
/// Panel Nahlad na tabuli: text upravovanym pismom, chybajuce znaky, kodovanie a nastavenia.
/// </summary>
[TestClass]
public sealed class BoardPreviewTests
{
    /// <summary>Proporcne pismo vysky 3: 'A' sirky 3 (lavy stlpec), 'B' sirky 2, ostatne znaky sirky 0.</summary>
    private static TableFont Font()
    {
        var font = TableFont.CreateEmpty("TEST", 8, 3, true);
        foreach (var symbol in font.Symbols)
            symbol.Width = 0;
        font.Symbols['A'].Width = 3;
        for (var r = 0; r < 3; r++)
            font.Symbols['A'][r, 0, 0] = true;
        font.Symbols['B'].Width = 2;
        return font;
    }

    private static string Info(BoardPreview preview) =>
        ((Label)typeof(BoardPreview).GetField("lInfo", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(preview)!).Text;

    [STATestMethod]
    public void Nahlad_TextPismom_ZnakyVedlaSebaAChybajuceVPopise()
    {
        using var preview = new BoardPreview();
        var settings = new BoardPreviewSettings { Text = "ABx" };
        preview.LoadSettings(settings);
        preview.ShowFont(Font(), SymbolCharset.Win1250);

        Assert.AreEqual(5, preview.Raster!.Width);
        Assert.IsTrue(preview.Raster.IsSet(0, 2));
        Assert.IsFalse(preview.Raster.IsSet(3, 0));
        StringAssert.Contains(Info(preview), string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.BoardPreview_Missing, "x"));
    }

    [STATestMethod]
    public void Nahlad_ZmenaTextu_ZapiseSaDoNastaveni()
    {
        using var preview = new BoardPreview();
        var settings = new BoardPreviewSettings();
        preview.LoadSettings(settings);

        var text = (TextBox)typeof(BoardPreview).GetField("tbText", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(preview)!;
        text.Text = "BA";

        Assert.AreEqual("BA", settings.Text);
    }

    [TestMethod]
    [DataRow(SymbolCharset.Win1250, (byte)0x8E)]
    [DataRow(SymbolCharset.Kamenicky, (byte)0x92)]
    public void Encode_ZnakPodlaKodovaniaStlpcaZnak(SymbolCharset charset, byte code)
    {
        var unsupported = new List<char>();

        CollectionAssert.AreEqual(new byte[] { (byte)'A', code }, BoardPreview.Encode("AŽ", charset, unsupported));
        Assert.IsEmpty(unsupported);
    }

    [TestMethod]
    public void Encode_ZnakMimoKodovania_NejdeNaTabulu()
    {
        var unsupported = new List<char>();

        // ő nie je v kode Kamenickych
        CollectionAssert.AreEqual(new byte[] { (byte)'A' }, BoardPreview.Encode("Aőő", SymbolCharset.Kamenicky, unsupported));
        CollectionAssert.AreEqual(new[] { 'ő' }, unsupported);
    }
}
