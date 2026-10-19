using ElenFontEditor.Entities;

namespace ElenFontEditor.Tests;

/// <summary>
/// Kopia pisma so zmenou rozmeru (sprava pisiem) a nove pismo.
/// </summary>
[TestClass]
public class TableFontTests
{
    /// <summary>Sirka nepouziteho znaku v realnych suboroch - vacsia ako sirka pisma.</summary>
    private const int UnusedWidth = 24;

    [TestMethod]
    public void Clone_BezZmeny_ZachovaSirkyAZnaky()
    {
        foreach (var proportional in new[] { true, false })
        {
            var font = CreateFont(proportional, 12, 9);

            var clone = font.Clone(font.MaxWidth, font.Height);

            for (var i = 0; i < font.Symbols.Length; i++)
            {
                Assert.AreEqual(font.Symbols[i].Width, clone.Symbols[i].Width, $"znak {i}");
                Assert.AreEqual(Bytes(font.Symbols[i]), Bytes(clone.Symbols[i]), $"znak {i}");
            }
        }
    }

    [TestMethod]
    public void Clone_ZnakyPatriaNovemuPismu()
    {
        var font = CreateFont(true, 12, 9);

        var clone = font.Clone("NOVE", 16, 10, true);

        Assert.AreEqual("NOVE", clone.Name);
        for (var i = 0; i < clone.Symbols.Length; i++)
        {
            var symbol = clone.Symbols[i];
            Assert.AreSame(clone, symbol.Font);
            Assert.AreEqual(i, symbol.Index);
            Assert.AreEqual(10, symbol.Rows);
            Assert.AreEqual(2, symbol.Columns);
        }
    }

    [TestMethod]
    public void Clone_ZuzeniePisma_OrezeSirkyABody()
    {
        var font = CreateFont(true, 12, 9);
        font.Symbols[65][0, 0] = 0xFF;
        font.Symbols[65][0, 1] = 0xF0;

        var clone = font.Clone(6, 7);

        Assert.AreEqual(6, clone.Symbols[1].Width); // povodne 11
        Assert.AreEqual(5, clone.Symbols[7].Width); // uzsi ostane
        Assert.AreEqual(0, clone.Symbols[0].Width);
        Assert.AreEqual(6, clone.Symbols[255].Width); // nepouzity znak sirky 24
        Assert.AreEqual(1, clone.Symbols[65].Columns);
        Assert.AreEqual(7, clone.Symbols[65].Rows);
        Assert.AreEqual(0xFC, clone.Symbols[65][0, 0]);
    }

    [TestMethod]
    public void Clone_RozsireniePisma_ZachovaSirkyProporcionalneho()
    {
        var font = CreateFont(true, 8, 9);
        font.Symbols[66][8, 0] = 0x81;

        var clone = font.Clone(16, 12);

        for (var i = 0; i < font.Symbols.Length; i++)
            Assert.AreEqual(font.Symbols[i].Width, clone.Symbols[i].Width, $"znak {i}");
        Assert.AreEqual(0x81, clone.Symbols[66][8, 0]);
        Assert.AreEqual(0, clone.Symbols[66][8, 1]);
        Assert.AreEqual(0, clone.Symbols[66][11, 0]);
    }

    [TestMethod]
    public void Clone_NeproporcionalnePismo_PevnaSirkaIdeSPismom()
    {
        var font = CreateFont(false, 6, 10);

        var wider = font.Clone(8, 10);
        var narrower = font.Clone(5, 10);

        Assert.AreEqual(8, wider.Symbols[65].Width);
        Assert.AreEqual(0, wider.Symbols[0].Width);
        Assert.AreEqual(UnusedWidth, wider.Symbols[255].Width);
        Assert.AreEqual(5, narrower.Symbols[65].Width);
        Assert.AreEqual(5, narrower.Symbols[255].Width);
    }

    [TestMethod]
    public void Clone_ZmenaNaNeproporcionalne_ZnakyMajuSirkuPisma()
    {
        var font = CreateFont(true, 12, 9);

        var clone = font.Clone(font.Name, 12, 9, false);

        Assert.AreEqual(0, clone.Symbols[0].Width);
        Assert.IsTrue(clone.Symbols.Skip(1).All(symbol => symbol.Width == 12));
    }

    [TestMethod]
    public void Clone_PismoBezZnakov_VytvoriPrazdneZnaky()
    {
        var font = new TableFont { Name = "NOVE", MaxWidth = 6, Height = 10 };

        var clone = font.Clone(8, 10);

        Assert.IsTrue(clone.Symbols.All(symbol => symbol is { Width: 8, Rows: 10, Columns: 1 } && symbol[0, 0] == 0));
    }

    [TestMethod]
    public void CreateEmpty_PrazdneZnakySoSirkouPisma()
    {
        var font = TableFont.CreateEmpty("NOVE", 10, 16, false);

        Assert.AreEqual("NOVE", font.Name);
        for (var i = 0; i < font.Symbols.Length; i++)
        {
            var symbol = font.Symbols[i];
            Assert.AreSame(font, symbol.Font);
            Assert.AreEqual(i, symbol.Index);
            Assert.AreEqual(10, symbol.Width);
            Assert.AreEqual(16, symbol.Rows);
            Assert.AreEqual(2, symbol.Columns);
            Assert.AreEqual("", Bytes(symbol).Trim('0'));
        }
    }

    [TestMethod]
    public void MaxWidth_PovolenaAzPoLimit()
    {
        var font = new TableFont { MaxWidth = TableFont.MaxWidthLimit };

        // dve matice - INISS2 cita najviac 2 bajty na riadok
        Assert.AreEqual(2, font.MatriciesCount);
        Assert.ThrowsExactly<ArgumentException>(() => font.MaxWidth = TableFont.MaxWidthLimit + 1);
        Assert.ThrowsExactly<ArgumentException>(() => font.MaxWidth = -1);
    }

    /// <summary>
    /// Pismo ako v realnych suboroch: znak 0 nie je (sirka 0), znak 255 je nepouzity so sirkou 24, ostatne maju pri
    /// proporcionalnom pisme rozne sirky, pri neproporcionalnom sirku pisma.
    /// </summary>
    private static TableFont CreateFont(bool proportional, int maxWidth, int height)
    {
        var font = new TableFont { Name = "TEST", MaxWidth = maxWidth, Height = height, IsProportional = proportional };
        for (var i = 0; i < font.Symbols.Length; i++)
        {
            var width = i switch
            {
                0 => 0,
                255 => UnusedWidth,
                _ => proportional ? maxWidth - i % 8 : maxWidth
            };
            var symbol = new FontSymbol(i, width, font);
            symbol[i % height, symbol.Columns - 1] = (byte)i;
            font.Symbols[i] = symbol;
        }

        return font;
    }

    private static string Bytes(FontSymbol symbol)
    {
        var bytes = new List<byte>();
        for (var r = 0; r < symbol.Rows; r++)
            for (var c = 0; c < symbol.Columns; c++)
                bytes.Add(symbol[r, c]);
        return Convert.ToHexString(bytes.ToArray());
    }
}
