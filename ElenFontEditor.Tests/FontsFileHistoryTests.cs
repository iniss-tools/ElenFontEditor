using System.Globalization;
using System.Text;
using ElenFontEditor.Entities;
using ElenFontEditor.History;

namespace ElenFontEditor.Tests;

/// <summary>
/// Kroky spat a znova nad otvorenym suborom pisiem.
/// </summary>
[TestClass]
public class FontsFileHistoryTests
{
    [TestMethod]
    public void Commit_BezZmeny_NevytvoriKrok()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: true);

        Assert.IsFalse(history.Commit("nic"));
        Assert.IsFalse(history.CanUndo);
        Assert.IsTrue(history.IsSaved);
    }

    [TestMethod]
    public void Undo_VratiBodZnaku_RedoHoZopakuje()
    {
        var file = CreateFile();
        var original = Dump(file);
        var history = new FontsFileHistory(file, saved: true);

        var point = file.Fonts[0].Symbols[65][2, 0, 3];
        file.Fonts[0].Symbols[65][2, 0, 3] = !point;
        var changed = Dump(file);
        Assert.IsTrue(history.Commit("bod"));

        history.Undo();
        Assert.AreEqual(point, file.Fonts[0].Symbols[65][2, 0, 3]);
        Assert.AreEqual(original, Dump(file));

        history.Redo();
        Assert.AreEqual(!point, file.Fonts[0].Symbols[65][2, 0, 3]);
        Assert.AreEqual(changed, Dump(file));
    }

    [TestMethod]
    public void Undo_VratiSirkuZnaku()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: true);
        var width = file.Fonts[1].Symbols[200].Width;

        file.Fonts[1].Symbols[200].Width = width - 1;
        Assert.IsTrue(history.Commit("sirka"));
        history.Undo();

        Assert.AreEqual(width, file.Fonts[1].Symbols[200].Width);
    }

    [TestMethod]
    public void Undo_VratiNazovCestuAPisma()
    {
        var file = CreateFile();
        var original = Dump(file);
        var history = new FontsFileHistory(file, saved: true);

        // premenovanie a sprava pisiem (vymena pisiem za nove so zmenenym rozmerom)
        file.Name = "NOVY";
        file.PathName = @"C:\fonty\NOVY.bin";
        var resized = file.Fonts[0].Clone(16, 10);
        file.Fonts.Clear();
        file.Fonts.Add(resized);
        Assert.IsTrue(history.Commit("sprava"));

        history.Undo();
        Assert.AreEqual("TEST", file.Name);
        Assert.AreEqual(@"C:\fonty\TEST.bin", file.PathName);
        Assert.HasCount(2, file.Fonts);
        Assert.AreEqual(original, Dump(file));

        history.Redo();
        Assert.AreEqual("NOVY", file.Name);
        Assert.HasCount(1, file.Fonts);
        Assert.AreEqual(16, file.Fonts[0].MaxWidth);
        Assert.AreEqual(10, file.Fonts[0].Height);
    }

    [TestMethod]
    public void Undo_ObnoveneZnakyPatriaObnovenemuPismu()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: true);
        file.Fonts[1].Symbols[0][0, 0] = 0xFF;
        history.Commit("bod");

        history.Undo();

        foreach (var font in file.Fonts)
        {
            for (var i = 0; i < font.Symbols.Length; i++)
            {
                var symbol = font.Symbols[i];
                Assert.AreSame(font, symbol.Font);
                Assert.AreEqual(i, symbol.Index);
                Assert.AreEqual(font.Height, symbol.Rows);
                Assert.AreEqual(font.MatriciesCount, symbol.Columns);
            }
        }
    }

    [TestMethod]
    public void Undo_NezapamatanaUpravaObnovenychZnakovNezmeniKroky()
    {
        var file = CreateFile();
        var original = Dump(file);
        var history = new FontsFileHistory(file, saved: true);
        file.Fonts[0].Symbols[10][0, 0] = 0x81;
        history.Commit("a");
        file.Fonts[0].Symbols[11][0, 0] = 0x42;
        history.Commit("b");
        var afterB = Dump(file);

        history.Undo(2);
        file.Fonts[0].Symbols[12][1, 0] = 0x18;
        history.Redo(2);
        Assert.AreEqual(afterB, Dump(file));

        history.Undo(2);
        Assert.AreEqual(original, Dump(file));
    }

    [TestMethod]
    public void Commit_PoKrokuSpat_ZahodiKrokyZnova()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: true);
        Toggle(file, 1);
        history.Commit("a");
        Toggle(file, 2);
        history.Commit("b");

        history.Undo();
        Toggle(file, 3);
        history.Commit("c");

        Assert.IsFalse(history.CanRedo);
        CollectionAssert.AreEqual(new[] { "c", "a" }, history.UndoLabels.ToArray());
    }

    [TestMethod]
    public void Labels_SpatOdPoslednehoZnovaOdNajblizsieho()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: true);
        foreach (var (label, symbol) in new[] { ("a", 1), ("b", 2), ("c", 3) })
        {
            Toggle(file, symbol);
            history.Commit(label);
        }

        Assert.AreEqual("c", history.UndoLabel);
        CollectionAssert.AreEqual(new[] { "c", "b", "a" }, history.UndoLabels.ToArray());

        history.Undo(2);
        Assert.AreEqual("a", history.UndoLabel);
        Assert.AreEqual("b", history.RedoLabel);
        CollectionAssert.AreEqual(new[] { "b", "c" }, history.RedoLabels.ToArray());
    }

    [TestMethod]
    public void IsSaved_SledujeUlozenyStav()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: true);

        Toggle(file, 1);
        history.Commit("a");
        Assert.IsFalse(history.IsSaved);
        history.Undo();
        Assert.IsTrue(history.IsSaved);
        history.Redo();
        history.MarkSaved();
        Assert.IsTrue(history.IsSaved);

        // ulozeny stav medzi zahodenymi krokmi znova sa uz neda dosiahnut
        history.Undo();
        Toggle(file, 2);
        history.Commit("b");
        Assert.IsFalse(history.IsSaved);
        history.Undo();
        Assert.IsFalse(history.IsSaved);
    }

    [TestMethod]
    public void IsSaved_NovySuborNieJeUlozeny()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: false);
        Assert.IsFalse(history.IsSaved);

        Toggle(file, 1);
        history.Commit("a");
        history.Undo();
        Assert.IsFalse(history.IsSaved);
    }

    [TestMethod]
    public void Commit_PamataNajviacCapacityKrokov()
    {
        var file = CreateFile();
        var history = new FontsFileHistory(file, saved: true);
        const int extra = 5;
        for (var i = 0; i < FontsFileHistory.Capacity + extra; i++)
        {
            Toggle(file, i);
            Assert.IsTrue(history.Commit(i.ToString(CultureInfo.InvariantCulture)));
        }

        Assert.AreEqual(FontsFileHistory.Capacity, history.Count);
        Assert.AreEqual((FontsFileHistory.Capacity + extra - 1).ToString(CultureInfo.InvariantCulture), history.UndoLabel);

        // najstarsi zapamatany stav = po prvych krokoch, ktore uz vratit nejde
        history.Undo(FontsFileHistory.Capacity);
        Assert.IsFalse(history.CanUndo);
        Assert.IsFalse(history.IsSaved);
        var expected = CreateFile();
        for (var i = 0; i < extra; i++)
            Toggle(expected, i);
        Assert.AreEqual(Dump(expected), Dump(file));
    }

    [TestMethod]
    public void Undo_MimoRozsahu_Vyhodi()
    {
        var history = new FontsFileHistory(CreateFile(), saved: true);

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => history.Undo());
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => history.Redo());
    }

    private static FontsFile CreateFile()
    {
        var file = new FontsFile { Name = "TEST", PathName = @"C:\fonty\TEST.bin" };
        file.Fonts.Add(CreateFont("Normal", 8, 7));
        file.Fonts.Add(CreateFont("Siroke", 12, 9));
        return file;
    }

    private static TableFont CreateFont(string name, int maxWidth, int height)
    {
        var font = new TableFont { Name = name, MaxWidth = maxWidth, Height = height, IsProportional = true };
        for (var i = 0; i < font.Symbols.Length; i++)
        {
            var symbol = new FontSymbol(i, maxWidth - i % 3, font);
            symbol[i % height, 0] = (byte)i;
            font.Symbols[i] = symbol;
        }

        return font;
    }

    /// <summary>Prepne bod v prvom riadku znaku <paramref name="symbol" /> (podla poradia, cez 256 aj v dalsom bite).</summary>
    private static void Toggle(FontsFile file, int symbol)
    {
        var target = file.Fonts[0].Symbols[symbol % 256];
        var bit = symbol / 256;
        target[0, 0, bit] = !target[0, 0, bit];
    }

    /// <summary>Cely obsah suboru ako text - nezavisle od porovnania v historii.</summary>
    private static string Dump(FontsFile file)
    {
        var text = new StringBuilder();
        text.AppendLine(CultureInfo.InvariantCulture, $"{file.Name}|{file.PathName}");
        foreach (var font in file.Fonts)
        {
            text.AppendLine(CultureInfo.InvariantCulture, $"{font.Name}|{font.MaxWidth}|{font.Height}|{font.IsProportional}");
            foreach (var symbol in font.Symbols)
            {
                text.Append(CultureInfo.InvariantCulture, $"{symbol.Index}:{symbol.Width}:{symbol.Rows}x{symbol.Columns}:");
                for (var r = 0; r < symbol.Rows; r++)
                    for (var c = 0; c < symbol.Columns; c++)
                        text.Append(symbol[r, c].ToString("X2", CultureInfo.InvariantCulture));
                text.AppendLine();
            }
        }

        return text.ToString();
    }
}
