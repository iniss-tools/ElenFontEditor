using System.Globalization;
using ElenFontEditor.Properties;
using ElenFontEditor.Tools;
using ToolsCore.Iniss.Elen;

namespace ElenFontEditor.Tests;

/// <summary>
/// Otvorenie suboru pisiem v editore.
/// </summary>
[TestClass]
public class ElenBinParserTests
{
    private DirectoryInfo _dir = null!;

    [TestInitialize]
    public void Init() => _dir = Directory.CreateTempSubdirectory("efe-parser-");

    [TestCleanup]
    public void Cleanup() => _dir.Delete(true);

    [TestMethod]
    public void LoadFile_PismoSirsieAkoEditor_ChybaSNazvomPisma()
    {
        // pismo so sirkou 24 (3 bajty na riadok) volne citanie prijme, editor ho neupravi
        var glyphs = Enumerable.Range(0, ElenFont.GlyphCount).Select(_ => new ElenGlyph(24, 2, 3, new byte[6])).ToArray();
        var path = Path.Combine(_dir.FullName, "SIROKE.bin");
        new ElenFontFile("SIROKE", [new ElenFont("SIROKE_24", 24, 2, false, glyphs)]).Save(path);

        var e = Assert.ThrowsExactly<FormatException>(() => ElenBinParser.LoadFile(path, strict: false));

        Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, Resources.FMain_FontTooWide, "SIROKE_24", 24, 16), e.Message);
    }

    [TestMethod]
    public void LoadFile_PoskodenySubor_ChybaZoZdrojovToolsCore()
    {
        var path = Path.Combine(_dir.FullName, "KRATKY.bin");
        File.WriteAllBytes(path, new byte[10]);

        var e = Assert.ThrowsExactly<FormatException>(() => ElenBinParser.LoadFile(path, strict: true));

        // text chyby nie je anglicky (vynimky ToolsCore.Iniss maju texty SK/CZ)
        Assert.DoesNotContain("File", e.Message);
        Assert.Contains("10 B", e.Message);
    }
}
