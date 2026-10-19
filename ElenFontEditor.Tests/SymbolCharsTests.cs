using System.Globalization;
using ElenFontEditor.Entities;
using ElenFontEditor.Forms;
using ElenFontEditor.Properties;
using ElenFontEditor.XML;
using ToolsCore.XML;

namespace ElenFontEditor.Tests;

/// <summary>
/// Text stlpca Znak a popisu kroku spat podla kodovania z nastaveni.
/// </summary>
[TestClass]
public class SymbolCharsTests
{
    [TestMethod]
    [DataRow(0x41, "A")]
    [DataRow(0x3F, "?")]
    [DataRow(0x8A, "Š")]
    [DataRow(0x9A, "š")]
    [DataRow(0xBE, "ľ")]
    [DataRow(0xE8, "č")]
    [DataRow(0xF4, "ô")]
    public void Text_Win1250_ZnakPodlaKodu(int code, string expected) =>
        Assert.AreEqual(expected, SymbolChars.Text(code, SymbolCharset.Win1250));

    [TestMethod]
    [DataRow(0x41, "A")]
    [DataRow(0x87, "č")]
    [DataRow(0x9A, "Ü")]
    [DataRow(0x9B, "Š")]
    [DataRow(0xA8, "š")]
    [DataRow(0x93, "ô")]
    public void Text_Kamenicky_ZnakPodlaKodu(int code, string expected) =>
        Assert.AreEqual(expected, SymbolChars.Text(code, SymbolCharset.Kamenicky));

    [TestMethod]
    [DataRow(0x00)]
    [DataRow(0x05)]
    [DataRow(0x1F)]
    [DataRow(0x7F)]
    public void Text_RiadiaciKod_PrazdnyText(int code)
    {
        Assert.AreEqual("", SymbolChars.Text(code, SymbolCharset.Win1250));
        Assert.AreEqual("", SymbolChars.Text(code, SymbolCharset.Kamenicky));
    }

    [TestMethod]
    [DataRow(0x81)]
    [DataRow(0x83)]
    [DataRow(0x88)]
    [DataRow(0x90)]
    [DataRow(0x98)]
    public void Text_Win1250NedefinovanaPozicia_PrazdnyText(int code) =>
        Assert.AreEqual("", SymbolChars.Text(code, SymbolCharset.Win1250));

    [TestMethod]
    public void Text_KodMimoBajtu_PrazdnyText()
    {
        Assert.AreEqual("", SymbolChars.Text(-1, SymbolCharset.Win1250));
        Assert.AreEqual("", SymbolChars.Text(256, SymbolCharset.Kamenicky));
    }

    [TestMethod]
    public void Text_KazdyKod_NieJeNeviditelnyZnak()
    {
        foreach (var charset in Enum.GetValues<SymbolCharset>())
        {
            for (var code = 0; code <= byte.MaxValue; code++)
            {
                var text = SymbolChars.Text(code, charset);
                Assert.IsTrue(text.Length <= 1, $"{charset} 0x{code:X2}");
                Assert.IsFalse(text is [var c] && char.IsControl(c), $"{charset} 0x{code:X2}");
            }
        }
    }

    [TestMethod]
    [DataRow(0x20)]
    [DataRow(0xA0)]
    public void Printable_Medzera_Null(int code) => Assert.IsNull(SymbolChars.Printable(code, SymbolCharset.Win1250));

    [TestMethod]
    public void SymbolLabel_ZnakVKodovaniZNastaveni()
    {
        Assert.AreEqual(Format(Resources.History_SymbolChar, 'š'), FMain.SymbolLabel(0x9A, SymbolCharset.Win1250));
        Assert.AreEqual(Format(Resources.History_SymbolChar, 'Ü'), FMain.SymbolLabel(0x9A, SymbolCharset.Kamenicky));
        Assert.AreEqual(Format(Resources.History_SymbolChar, 'š'), FMain.SymbolLabel(0xA8, SymbolCharset.Kamenicky));
    }

    [TestMethod]
    [DataRow(0x05)]
    [DataRow(0x20)]
    [DataRow(0x81)]
    public void SymbolLabel_NetlacitelnyZnak_CisloZnaku(int code) =>
        Assert.AreEqual(Format(Resources.History_SymbolIndex, code), FMain.SymbolLabel(code, SymbolCharset.Win1250));

    [TestMethod]
    public void Config_KodovanieSaUlozi_AjVKopii()
    {
        var dir = Directory.CreateTempSubdirectory("efe-config-");
        try
        {
            var path = Path.Combine(dir.FullName, "config.xml");
            var config = new ElenFontEditorConfig { CharEncoding = SymbolCharset.Kamenicky };

            XmlSerialization.WriteData(path, config);

            StringAssert.Contains(File.ReadAllText(path), "<CharEncoding>Kamenicky</CharEncoding>");
            Assert.AreEqual(SymbolCharset.Kamenicky, XmlSerialization.ReadData<ElenFontEditorConfig>(path).CharEncoding);
            Assert.AreEqual(SymbolCharset.Kamenicky, (config with { }).CharEncoding);
        }
        finally
        {
            dir.Delete(true);
        }
    }

    [TestMethod]
    public void Config_BezKodovania_Windows1250()
    {
        var dir = Directory.CreateTempSubdirectory("efe-config-");
        try
        {
            var path = Path.Combine(dir.FullName, "config.xml");
            XmlSerialization.WriteData(path, new ElenFontEditorConfig());

            Assert.DoesNotContain("CharEncoding", File.ReadAllText(path));
            Assert.AreEqual(SymbolCharset.Win1250, XmlSerialization.ReadData<ElenFontEditorConfig>(path).CharEncoding);
        }
        finally
        {
            dir.Delete(true);
        }
    }

    private static string Format(string format, object value) => string.Format(CultureInfo.CurrentCulture, format, value);
}
