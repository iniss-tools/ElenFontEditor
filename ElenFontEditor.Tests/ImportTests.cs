using System.Drawing;
using ElenFontEditor.Entities;
using ElenFontEditor.Import;

namespace ElenFontEditor.Tests;

/// <summary>
/// Import znakov z pisma Windows a z obrazka: prispôsobenie bodov pismu, mriezka obrazka a zlozenie pisma.
/// </summary>
[TestClass]
public sealed class ImportTests
{
    /// <summary>Body z textu: riadky oddelene '|', '#' = bod svieti.</summary>
    private static bool[,] Dots(string text)
    {
        var rows = text.Split('|');
        var dots = new bool[rows.Length, rows.Max(row => row.Length)];
        for (var r = 0; r < rows.Length; r++)
            for (var c = 0; c < rows[r].Length; c++)
                dots[r, c] = rows[r][c] == '#';
        return dots;
    }

    private static string Text(bool[,] dots) =>
        string.Join("|", Enumerable.Range(0, dots.GetLength(0)).Select(r =>
            new string(Enumerable.Range(0, dots.GetLength(1)).Select(c => dots[r, c] ? '#' : '.').ToArray())));

    [TestMethod]
    public void Fit_Proporcne_OrezeZlavaASirkaPodlaBodovAMedzery()
    {
        var layout = new GlyphLayout(3, 8, true, Spacing: 1);

        var glyph = layout.Fit(65, Dots("..#.#|..##.|..#.#"), 9);

        Assert.AreEqual("#.#.....|##......|#.#.....", Text(glyph.Dots));
        Assert.AreEqual(4, glyph.Width);
    }

    [TestMethod]
    public void Fit_Neproporcne_NaStredASirkaPisma()
    {
        var layout = new GlyphLayout(1, 8, false, Spacing: 1, Center: true);

        var glyph = layout.Fit(73, Dots("....###"), 9);

        // volne miesto 8 - 1 (medzera) - 3 = 4 -> 2 body zlava
        Assert.AreEqual("..###...", Text(glyph.Dots));
        Assert.AreEqual(8, glyph.Width);
    }

    [TestMethod]
    public void Fit_ZnakBezBodov_SirkaZoZdroja()
    {
        var glyph = new GlyphLayout(2, 8, true).Fit(32, new bool[2, 10], 3);

        Assert.IsFalse(glyph.HasDots);
        Assert.AreEqual(3, glyph.Width);
    }

    [TestMethod]
    public void Fit_SirsiAkoPismo_OdrezeSaAOdsadenieZlava()
    {
        var glyph = new GlyphLayout(1, 4, true, Spacing: 1, Left: 1).Fit(87, Dots("######"), 6);

        Assert.AreEqual(".###", Text(glyph.Dots));
        Assert.AreEqual(4, glyph.Width);
    }

    [TestMethod]
    public void Mriezka_PocetBuniekAOkraje()
    {
        var grid = new ImageGrid(1, 4, 5, 3, Left: 2, Top: 1, GapX: 1, GapY: 2);

        // sirka 2 + 3 × 4 + 2 medzery = 16, vyska 1 + 2 × 5 + 1 medzera = 13 (dalsi riadok sa nezmesti)
        Assert.AreEqual(6, grid.CellCount(new Size(16, 13)));
        Assert.AreEqual(new Point(7, 8), grid.CellOrigin(4));
    }

    [TestMethod]
    public void Obrazok_TmaveBodyNaBielom_BunkaSoZvacsenimAPrahom()
    {
        // 2 bunky 4 × 2 px vedla seba, bod = 2 × 2 px; v druhej bunke je lavy bod cierny, pravy z polovice sivy
        using var image = new Bitmap(8, 2);
        using (var g = Graphics.FromImage(image))
            g.Clear(Color.White);
        for (var y = 0; y < 2; y++)
        {
            image.SetPixel(4, y, Color.Black);
            image.SetPixel(5, y, Color.Black);
            image.SetPixel(6, y, Color.Black);
        }

        Assert.IsTrue(PixelCoverage.HasDarkInk(image));
        var coverage = PixelCoverage.FromImage(image, darkInk: true);
        var grid = new ImageGrid(2, 4, 2, 2);

        Assert.AreEqual("..", Text(grid.Read(coverage, 0, 50)));
        Assert.AreEqual("##", Text(grid.Read(coverage, 1, 50)));
        Assert.AreEqual("#.", Text(grid.Read(coverage, 1, 75)));
    }

    [TestMethod]
    public void Obrazok_SvetlaKresbaNaPriehladnom_KresbaSvetla()
    {
        using var image = new Bitmap(4, 4);
        image.SetPixel(1, 1, Color.White);

        Assert.IsFalse(PixelCoverage.HasDarkInk(image));
        var dots = ImageGrid.Single(image.Size, 1).Read(PixelCoverage.FromImage(image, darkInk: false), 0, 50);
        Assert.AreEqual("....|.#..|....|....", Text(dots));
    }

    [TestMethod]
    public void Zlozenie_NoveProporcnePismo_NeimportovaneZnakySirky0()
    {
        var file = new FontsFile { Name = "F", PathName = "F.bin" };
        var glyph = new GlyphLayout(2, 8, true).Fit(65, Dots("#.|##"), 3);

        var font = SymbolImport.Build(file, new ImportTarget(-1, "NOVE", 8, 2, true, false), [glyph], out var imported);

        CollectionAssert.AreEqual(new[] { 65 }, imported);
        Assert.AreEqual("NOVE", font.Name);
        Assert.AreEqual(3, font.Symbols[65].Width);
        Assert.AreEqual(0, font.Symbols[66].Width);
        Assert.IsTrue(font.Symbols[65][1, 0, 1]);
        Assert.IsFalse(font.Symbols[65][0, 0, 1]);
    }

    [TestMethod]
    public void Zlozenie_LenPrazdneZnaky_ZnakSBodmiOstane()
    {
        var existing = TableFont.CreateEmpty("POVODNE", 6, 2, false);
        existing.Symbols[65][0, 0, 0] = true;
        var file = new FontsFile { Name = "F", PathName = "F.bin" };
        file.Fonts.Add(existing);
        var layout = new GlyphLayout(2, 6, false);
        ImportGlyph[] glyphs = [layout.Fit(65, Dots("##|##"), 2), layout.Fit(66, Dots("##|##"), 2)];

        var font = SymbolImport.Build(file, new ImportTarget(0, "", 0, 0, false, true), glyphs, out var imported);

        CollectionAssert.AreEqual(new[] { 66 }, imported);
        Assert.AreNotSame(existing, font);
        Assert.IsFalse(font.Symbols[65][1, 0, 0]);
        Assert.IsTrue(font.Symbols[66][1, 0, 1]);
        Assert.AreEqual(6, font.Symbols[66].Width);
        Assert.IsFalse(existing.Symbols[66][0, 0, 0], "povodne pismo sa nemeni");
    }

    [TestMethod]
    public void PismoWindows_ZnakMedzeraAChybajuci()
    {
        var family = FontFamily.GenericSansSerif;
        var (size, baseline) = TrueTypeOptions.Defaults(family, FontStyle.Regular, 16);
        using var rasterizer = new TrueTypeRasterizer(family, new TrueTypeOptions(FontStyle.Regular, size, baseline), 16);

        var letter = rasterizer.Render('H');
        var space = rasterizer.Render(' ');

        Assert.IsNotNull(letter);
        Assert.IsTrue(letter.Value.Ink.Cast<bool>().Any(dot => dot));
        Assert.IsGreaterThan(0, letter.Value.Advance);
        Assert.IsNotNull(space);
        Assert.IsFalse(space.Value.Ink.Cast<bool>().Any(dot => dot));
        Assert.IsGreaterThan(0, space.Value.Advance);
        // znak zo sukromnej oblasti Unicode bezne pisma nemaju
        Assert.IsNull(rasterizer.Render(''));
    }
}
