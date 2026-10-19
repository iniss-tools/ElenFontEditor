using System.Reflection;
using ElenFontEditor.Controls;
using ElenFontEditor.Entities;
using ElenFontEditor.Forms;
using ExControls;

namespace ElenFontEditor.DocScreenshots;

/// <summary>
/// Zoznam snímok. Každá snímka je okno uložené ako <c>&lt;priečinok&gt;/&lt;názov&gt;-light.png</c> a <c>-dark.png</c>;
/// priečinok je názov článku v <c>docs/elenfonteditor</c>.
/// </summary>
/// <remarks>
/// Ak premenná prostredia <c>DOCSHOTS_BOUNDS</c> určuje súbor, pripíšu sa doň polohy prvkov každej snímky
/// v percentách – podľa nich sa v článkoch kreslia číslované značky.
/// </remarks>
internal sealed class Shots(Program.Options options, string theme, List<string> log)
{
    private const BindingFlags Any = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>Tučné proporčné písmo ukážkového súboru – znaky majú dve mriežky, editor je štvorcový.</summary>
    private const int BoldFont = 2;

    private int _count;

    public int Run(string path)
    {
        log.Add($"krok: otvorenie hlavného okna ({theme})");
        var main = Program.OpenMain(path);

        try
        {
            var file = Field<FontsFile>(main, "_openedFile");

            // hlavné okno: tučné písmo, znak G a poloha bodu pod myšou v stavovom riadku
            Shot("hlavne-okno/hlavne-okno", main, _ =>
            {
                SelectSymbol(main, BoldFont, 'G');
                PointAt(main, 4, 6);
            }, dispose: false);

            // označené body – obdĺžnik cez hornú časť znaku
            Shot("uprava-znakov/oznacene-body", main, _ =>
            {
                SelectSymbol(main, BoldFont, 'A');
                SelectPoints(main, 2, 0, 6, 9);
                PointAt(main, 6, 9);
            }, dispose: false);

            Shot("sprava-pisiem/sprava-pisiem", () => new FFontManager(file), form => Field<DataGridView>(form, "dgvFonts").ClearSelection());

            Shot("ukladanie/premenovanie-suboru", () => new FRenameFile(file, Field<string?>(main, "_diskPath")), form =>
            {
                Field<ExTextBox>(form, "tbNewName").Text = "PISMA_16_1250";
                Unselect(form);
            });

            // stránky nastavení programu - rovnaké ako v GVDEditore, stránka Všeobecné má navyše skupinu Program
            foreach (var (slug, panel) in new[]
                     {
                         ("nastavenia-programu", ""),
                         ("komponenty", "pDesktopComponents"),
                         ("stlpce", "pDesktopColumns"),
                         ("lokalizacia", "pLocalization"),
                         ("klavesove-skratky", "pShortcuts"),
                         ("styly", "pStyles"),
                         ("pisma", "pFonts"),
                         ("logovanie", "pLogging"),
                     })
            {
                Shot($"nastavenia-programu/{slug}", () =>
                {
                    var form = new FAppSettings(GlobData.Config, GlobData.Styles);
                    if (panel.Length > 0)
                        form.PreselectMenuItem(panel);
                    return form;
                });
            }
        }
        finally
        {
            // Dispose namiesto Close - Close by sa pri neuloženom súbore pýtal na uloženie
            main.Dispose();
        }

        return _count;
    }

    private void Shot(string name, Func<Form> create, Action<Form>? setup = null) => Shot(name, create(), setup);

    private void Shot(string name, Form form, Action<Form>? setup = null, bool dispose = true)
    {
        log.Add($"krok: {name} ({theme})");
        try
        {
            if (!form.Visible)
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(60, 60);
                form.ShowInTaskbar = false;
                form.Show();
            }

            Pump.Events();
            setup?.Invoke(form);
            Pump.Events();
            form.Refresh();
            Pump.Events();
            Save(name, form);
        }
        catch (Exception e)
        {
            log.Add($"{name} ({theme}): {e.GetType().Name}: {(e is TargetInvocationException { InnerException: { } inner } ? inner.Message : e.Message)}");
        }
        finally
        {
            if (dispose)
                form.Dispose();
        }
    }

    private void Save(string name, Form form)
    {
        if (options.Only is not null && !name.Contains(options.Only, StringComparison.OrdinalIgnoreCase))
            return;

        var file = Path.Combine(options.OutDir, $"{name}-{theme}.png".Replace('/', Path.DirectorySeparatorChar));
        WindowCapture.Save(form, file);
        WriteBounds(name, form);
        _count++;
    }

    /// <summary>
    /// Vyberie písmo a v ňom znak ako kliknutím do zoznamu; editor znaku zväčší tak, aby body boli štvorcové.
    /// </summary>
    private static void SelectSymbol(FMain main, int font, char symbol)
    {
        var fonts = Field<ExToolStripComboBox>(main, "tscomboFonts");
        if (fonts.SelectedIndex != font)
            fonts.SelectedIndex = font;
        Pump.Events();

        var grid = Field<DataGridView>(main, "dgvSymbols");
        var row = grid.Rows[symbol];
        grid.ClearSelection();
        grid.CurrentCell = row.Cells.Cast<DataGridViewCell>().First(c => c.Visible);
        row.Selected = true;
        grid.FirstDisplayedScrollingRowIndex = Math.Max(0, row.Index - 3);
        Pump.Events();

        // šírka panela editora podľa počtu bodov znaku (s hlavičkami riadkov a stĺpcov)
        var editor = Field<SymbolEditor>(main, "symbolEditor");
        var split = Field<SplitContainer>(main, "splitContainer1");
        var panel = split.Panel2;
        var size = (panel.Height - panel.Padding.Vertical) / (editor.Cells.RowsCount + 1);
        var width = size * (editor.Cells.ColumnsCount + 1) + panel.Padding.Horizontal;
        split.SplitterDistance = split.Width - split.SplitterWidth - width;
        Pump.Events();
    }

    /// <summary>
    /// Označí obdĺžnik bodov od <paramref name="top" />, <paramref name="left" /> po <paramref name="bottom" />,
    /// <paramref name="right" /> (vrátane) ako ťahaním myšou.
    /// </summary>
    private static void SelectPoints(FMain main, int top, int left, int bottom, int right)
    {
        var editor = Field<SymbolEditor>(main, "symbolEditor");
        editor.DeselectAll();
        for (var r = top; r <= bottom; r++)
            for (var c = left; c <= right; c++)
                editor.Cells[r, c].Selected = true;
        editor.Invalidate();
    }

    /// <summary>
    /// Poloha bodu v stavovom riadku ako pri pohybe myši nad editorom.
    /// </summary>
    private static void PointAt(FMain main, int row, int column)
    {
        var editor = Field<SymbolEditor>(main, "symbolEditor");
        var center = editor.Cells[row, column].Bounds;
        var args = new MouseEventArgs(MouseButtons.None, 0, center.Left + center.Width / 2, center.Top + center.Height / 2, 0);
        typeof(FMain).GetMethod("symbolEditor_MouseMove", Any)!.Invoke(main, [editor, args]);
    }

    /// <summary>
    /// Pre značky v článkoch: polohy prvkov okna v percentách snímky (ľavý okraj, stred, horný okraj). Zapíše sa,
    /// len ak premenná prostredia <c>DOCSHOTS_BOUNDS</c> určuje súbor.
    /// </summary>
    private void WriteBounds(string shot, Form form)
    {
        if (Environment.GetEnvironmentVariable("DOCSHOTS_BOUNDS") is not { Length: > 0 } file || theme != "light")
            return;

        var frame = WindowCapture.FrameBounds(form);
        var lines = new List<string>();

        string P(int v, int from, int size) => (100.0 * (v - from) / size).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);

        void Add(string name, string type, Rectangle r, string text) =>
            lines.Add($"{shot} {name} {type} left={P(r.Left, frame.Left, frame.Width)} cx={P(r.Left + r.Width / 2, frame.Left, frame.Width)} " +
                      $"top={P(r.Top, frame.Top, frame.Height)} cy={P(r.Top + r.Height / 2, frame.Top, frame.Height)} \"{text}\"");

        foreach (var control in Descendants(form).Where(c => c.Visible && c.Name.Length > 0 && c.Width > 0 && c.IsHandleCreated))
        {
            if (control is not (ButtonBase or DataGridView or TextBoxBase or ComboBox or UpDownBase or TreeView or ListBox or Label
                or ToolStrip or SymbolEditor or GroupBox))
                continue;

            Add(control.Name, control.GetType().Name, control.RectangleToScreen(control.ClientRectangle),
                control is Label or ButtonBase or GroupBox ? control.Text.Split('\n')[0].TrimEnd() : "");

            // tlačidlá a položky panelov nástrojov, ponuky a stavového riadku
            if (control is ToolStrip strip)
                foreach (ToolStripItem item in strip.Items)
                    if (item.Visible && item.Name is { Length: > 0 })
                    {
                        var r = item.Bounds;
                        r.Offset(strip.PointToScreen(Point.Empty));
                        Add(item.Name, item.GetType().Name, r, item.Text ?? "");
                    }
        }

        File.AppendAllLines(file, lines);
    }

    /// <summary>
    /// Bez zvýrazneného textu v poliach a s fokusom na hlavnom tlačidle.
    /// </summary>
    private static void Unselect(Form form)
    {
        foreach (var box in Descendants(form).OfType<TextBoxBase>())
            box.SelectionLength = 0;
        if (form.AcceptButton is Control accept)
            form.ActiveControl = accept;
    }

    private static T Field<T>(Form form, string name) =>
        (T)form.GetType().GetField(name, Any)!.GetValue(form)!;

    private static IEnumerable<Control> Descendants(Control control)
    {
        foreach (Control child in control.Controls)
        {
            yield return child;
            foreach (var nested in Descendants(child))
                yield return nested;
        }
    }
}
