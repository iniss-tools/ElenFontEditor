namespace ElenFontEditor.Import;

/// <summary>
/// Rozdelenie obrazka na bunky so znakmi (v pixeloch obrazka). Bunky idu po riadkoch zlava doprava.
/// </summary>
/// <param name="Scale">pixelov obrazka na jeden bod (zvacseny obrazok)</param>
/// <param name="CellWidth">sirka bunky</param>
/// <param name="CellHeight">vyska bunky</param>
/// <param name="Columns">pocet buniek v riadku</param>
/// <param name="Left">okraj obrazka vlavo</param>
/// <param name="Top">okraj obrazka hore</param>
/// <param name="GapX">medzera medzi bunkami vodorovne</param>
/// <param name="GapY">medzera medzi bunkami zvislo</param>
public sealed record ImageGrid(int Scale, int CellWidth, int CellHeight, int Columns, int Left = 0, int Top = 0, int GapX = 0, int GapY = 0)
{
    /// <summary>Cely obrazok ako jeden znak.</summary>
    public static ImageGrid Single(Size image, int scale) => new(scale, image.Width, image.Height, 1);

    /// <summary>Pocet celych buniek, ktore sa do obrazka zmestia.</summary>
    public int CellCount(Size image)
    {
        if (CellWidth <= 0 || CellHeight <= 0 || Columns <= 0)
            return 0;
        var columns = Math.Min(Columns, Math.Max(0, (image.Width - Left + GapX) / (CellWidth + GapX)));
        var rows = Math.Max(0, (image.Height - Top + GapY) / (CellHeight + GapY));
        return columns * rows;
    }

    /// <summary>Lavy horny roh bunky <paramref name="index" />.</summary>
    public Point CellOrigin(int index) =>
        new(Left + index % Columns * (CellWidth + GapX), Top + index / Columns * (CellHeight + GapY));

    /// <summary>
    /// Body bunky <paramref name="index" /> [riadok, stlpec]: bod svieti, ked kresba pokryva jeho stvorec pixelov
    /// aspon z <paramref name="threshold" /> percent.
    /// </summary>
    internal bool[,] Read(PixelCoverage coverage, int index, int threshold)
    {
        ArgumentNullException.ThrowIfNull(coverage);
        var scale = Math.Max(1, Scale);
        var origin = CellOrigin(index);
        var rows = CellHeight / scale;
        var columns = CellWidth / scale;
        var limit = Math.Clamp(threshold, 1, 99) / 100f;
        var dots = new bool[rows, columns];
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
                dots[r, c] = coverage.Average(origin.X + c * scale, origin.Y + r * scale, scale) >= limit;
        }

        return dots;
    }
}
