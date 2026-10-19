using ElenFontEditor.Entities;
using ElenFontEditor.Forms;
using ElenFontEditor.Tools;
using ToolsCore.Iniss.Elen;

namespace ElenFontEditor.Tests;

/// <summary>
/// Sprava pisiem: nove pismo, ulozenie upravenych kopii a kontrola nazvov a rozmerov.
/// </summary>
[TestClass]
public class FontManagerTests
{
    [TestMethod]
    public void CreateNewFont_MaPlatnyNazovKtoryVSuboreNieJe()
    {
        var fonts = new List<TableFont> { TableFont.CreateEmpty("PISMO_1", 6, 10, false), TableFont.CreateEmpty("PISMO_3", 6, 10, false) };

        var font = FFontManager.CreateNewFont(fonts);

        Assert.AreEqual("PISMO_2", font.Name);
        Assert.IsNull(FontRules.NameError(font.Name, fonts.Select(f => f.Name)));
        Assert.IsTrue(font.Symbols.All(symbol => symbol is not null && symbol.Font == font));
    }

    [TestMethod]
    public void CreateFonts_NovePismo_DaSaUlozit()
    {
        var dir = Directory.CreateTempSubdirectory("efe-test-");
        try
        {
            var file = new FontsFile { Name = "TEST", PathName = Path.Combine(dir.FullName, "TEST.bin") };
            // novy riadok mriezky a uprava jeho buniek
            var added = FFontManager.CreateNewFont(file.Fonts);
            added.Name = "NOVE";
            added.MaxWidth = 10;
            added.Height = 16;

            file.Fonts.AddRange(FFontManager.CreateFonts([added], new Dictionary<TableFont, TableFont>()));
            ElenBinParser.SaveFile(file);

            var saved = ElenFontFile.Load(file.PathName, strict: true);
            var font = saved.Fonts.Single();
            Assert.AreEqual("NOVE", font.Name);
            Assert.AreEqual(10, font.MaxWidth);
            Assert.AreEqual(16, font.Height);
            Assert.IsFalse(font.IsProportional);
            Assert.IsTrue(font.Glyphs.All(glyph => glyph.Width == 10 && glyph.Data.ToArray().All(b => b == 0)));
        }
        finally
        {
            dir.Delete(true);
        }
    }

    [TestMethod]
    public void CreateFonts_BezZmeny_ZachovaPismoAZnaky()
    {
        var original = CreateProportionalFont();
        var copy = original.Clone();

        var font = FFontManager.CreateFonts([copy], new Dictionary<TableFont, TableFont> { [copy] = original }).Single();

        Assert.AreNotSame(original, font);
        for (var i = 0; i < font.Symbols.Length; i++)
        {
            Assert.AreSame(font, font.Symbols[i].Font);
            Assert.AreEqual(original.Symbols[i].Width, font.Symbols[i].Width, $"znak {i}");
            Assert.AreEqual(original.Symbols[i][i % original.Height, 0], font.Symbols[i][i % original.Height, 0], $"znak {i}");
        }
    }

    [TestMethod]
    public void CreateFonts_ZuzenieVMriezke_OrezeSirkyPodlaPovodnehoPisma()
    {
        var original = CreateProportionalFont();
        var copy = original.Clone();
        // mriezka meni kopiu na mieste - povodna sirka pisma sa berie z pisma v subore
        copy.Name = "UZKE";
        copy.MaxWidth = 6;

        var font = FFontManager.CreateFonts([copy], new Dictionary<TableFont, TableFont> { [copy] = original }).Single();

        Assert.AreEqual("UZKE", font.Name);
        Assert.AreEqual(6, font.MaxWidth);
        Assert.AreEqual(1, font.MatriciesCount);
        Assert.IsTrue(font.Symbols.All(symbol => symbol.Width <= 6 && symbol.Columns == 1));
        Assert.AreEqual(5, font.Symbols[7].Width);
        Assert.AreEqual("PROP", original.Name);
        Assert.AreEqual(12, original.MaxWidth);
    }

    [TestMethod]
    [DataRow("", false)]
    [DataRow("   ", false)]
    [DataRow("PISMO_10_7", true)]
    [DataRow(" PISMO ", true)]
    [DataRow("ABCDEFGHIJKLMNOPQRST", true)]
    [DataRow("ABCDEFGHIJKLMNOPQRSTU", false)]
    [DataRow("PÍSMO", false)]
    [DataRow("DUPLICITA", false)]
    [DataRow("duplicita", true)]
    public void NameError_KontrolujeNazovPisma(string name, bool valid)
    {
        var error = FontRules.NameError(name, ["INE", "DUPLICITA"]);

        Assert.AreEqual(valid, error is null, error);
    }

    [TestMethod]
    [DataRow("1", TableFont.MaxWidthLimit, true)]
    [DataRow("24", TableFont.MaxWidthLimit, true)]
    [DataRow("25", TableFont.MaxWidthLimit, false)]
    [DataRow("20", TableFont.HeightLimit, true)]
    [DataRow("21", TableFont.HeightLimit, false)]
    [DataRow("0", TableFont.MaxWidthLimit, false)]
    [DataRow("-3", TableFont.MaxWidthLimit, false)]
    [DataRow("abc", TableFont.MaxWidthLimit, false)]
    public void SizeError_KontrolujeRozmer(string text, int max, bool valid)
    {
        Assert.AreEqual(valid, FontRules.SizeError(text, max) is null);
    }

    [TestMethod]
    public void FindProblem_NajviacSestnastPisiemSRoznymiNazvami()
    {
        var fonts = Enumerable.Range(1, ElenFontFile.MaxFonts).Select(i => TableFont.CreateEmpty($"F{i}", 6, 10, false)).ToList();
        Assert.IsNull(FontRules.FindProblem(fonts));

        fonts.Add(TableFont.CreateEmpty("F17", 6, 10, false));
        Assert.IsNotNull(FontRules.FindProblem(fonts));

        fonts.RemoveAt(fonts.Count - 1);
        fonts[3].Name = "F1";
        Assert.IsNotNull(FontRules.FindProblem(fonts));
    }

    /// <summary>Proporcionalne pismo 12 × 9 so sirkami 5-12 a jednym bodom v kazdom znaku.</summary>
    private static TableFont CreateProportionalFont()
    {
        var font = new TableFont { Name = "PROP", MaxWidth = 12, Height = 9, IsProportional = true };
        for (var i = 0; i < font.Symbols.Length; i++)
        {
            var symbol = new FontSymbol(i, 12 - i % 8, font);
            symbol[i % font.Height, 0] = 0x40;
            font.Symbols[i] = symbol;
        }

        return font;
    }
}
