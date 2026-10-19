using ElenFontEditor.Controls;
using ElenFontEditor.Entities;

namespace ElenFontEditor.Tests;

/// <summary>
/// Kopirovanie a prilepenie bodov v editore znaku.
/// </summary>
[TestClass]
public class SymbolEditorTests
{
    /// <summary>Proporcne pismo 8 × 4 body; znak 0 ma body v rohoch.</summary>
    private static TableFont Font()
    {
        var font = TableFont.CreateEmpty("A", 8, 4, true);
        font.Symbols[0][0, 0, 0] = true;
        font.Symbols[0][3, 0, 7] = true;
        return font;
    }

    private static bool[] Points(FontSymbol symbol) =>
        Enumerable.Range(0, symbol.Rows).SelectMany(r => Enumerable.Range(0, 8).Select(c => symbol[r, 0, c])).ToArray();

    [STATestMethod]
    public void Copy_CelyZnakOznacenyRohom_PrilepiVsetkyBody()
    {
        var font = Font();
        using var editor = new SymbolEditor();
        editor.SetEditingSymbol(font.Symbols[0]);
        editor.SelectAll();
        editor.Copy();

        editor.SetEditingSymbol(font.Symbols[1]);
        editor.Paste();
        editor.GetEditedSymbol();

        CollectionAssert.AreEqual(Points(font.Symbols[0]), Points(font.Symbols[1]));
    }

    [STATestMethod]
    public void Copy_NepravidelneOznacenie_PolohaVociLavemuHornemuRohu()
    {
        // bod vpravo hore a bod vlavo dole - prvy oznaceny bod nie je lavy horny roh oznacenia
        var font = Font();
        var source = font.Symbols[2];
        source[0, 0, 1] = true;
        source[1, 0, 0] = true;
        using var editor = new SymbolEditor();
        editor.SetEditingSymbol(source);
        editor.Cells[0, 1].Selected = true;
        editor.Cells[1, 0].Selected = true;
        editor.Copy();

        editor.SetEditingSymbol(font.Symbols[3]);
        editor.Paste();
        editor.GetEditedSymbol();

        Assert.IsTrue(font.Symbols[3][0, 0, 1]);
        Assert.IsTrue(font.Symbols[3][1, 0, 0]);
        Assert.AreEqual(2, Points(font.Symbols[3]).Count(p => p));
    }

    [STATestMethod]
    public void Paste_DoOznacenehoBodu_PresahOreze()
    {
        var font = Font();
        using var editor = new SymbolEditor();
        editor.SetEditingSymbol(font.Symbols[0]);
        editor.SelectAll();
        editor.Copy();

        // prilepenie od predposledneho riadka a stlpca - zmesti sa len lavy horny roh predlohy
        editor.SetEditingSymbol(font.Symbols[4]);
        editor.Cells[2, 6].Selected = true;
        editor.Paste();
        editor.GetEditedSymbol();

        Assert.IsTrue(font.Symbols[4][2, 0, 6]);
        Assert.AreEqual(1, Points(font.Symbols[4]).Count(p => p));
    }

    [STATestMethod]
    public void Cut_ZhasneOznaceneBody()
    {
        var font = Font();
        using var editor = new SymbolEditor();
        editor.SetEditingSymbol(font.Symbols[0]);
        editor.SelectAll();
        editor.Cut();
        editor.GetEditedSymbol();

        Assert.IsFalse(Points(font.Symbols[0]).Any(p => p));
    }
}
