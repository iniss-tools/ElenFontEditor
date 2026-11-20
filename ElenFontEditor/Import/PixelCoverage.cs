using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ElenFontEditor.Import;

/// <summary>
/// Pokrytie bodov obrazka kresbou (0 = pozadie, 1 = cela kresba) - spolocne pre pismo Windows aj obrazok.
/// </summary>
internal sealed class PixelCoverage
{
    private readonly float[] _values;

    private PixelCoverage(int width, int height, float[] values)
    {
        Width = width;
        Height = height;
        _values = values;
    }

    /// <summary>Sirka v pixeloch.</summary>
    public int Width { get; }

    /// <summary>Vyska v pixeloch.</summary>
    public int Height { get; }

    /// <summary>
    /// Pokrytie z jasu obrazka: svetla kresba na tmavom pozadi, pri <paramref name="darkInk" /> tmava na svetlom.
    /// Priehladne pixely su pozadie.
    /// </summary>
    public static PixelCoverage FromImage(Bitmap image, bool darkInk)
    {
        ArgumentNullException.ThrowIfNull(image);
        var argb = ReadArgb(image);
        var values = new float[argb.Length];
        for (var i = 0; i < argb.Length; i++)
        {
            var p = argb[i];
            var alpha = ((p >> 24) & 0xFF) / 255f;
            var luminance = (0.299f * ((p >> 16) & 0xFF) + 0.587f * ((p >> 8) & 0xFF) + 0.114f * (p & 0xFF)) / 255f;
            var ink = darkInk ? 1 - luminance : luminance;
            values[i] = ink * alpha;
        }

        return new PixelCoverage(image.Width, image.Height, values);
    }

    /// <summary>
    /// Ci kresbu obrazka tvoria tmave body: pri nepriehladnom pozadi, ked je obrazok prevazne svetly (pismo na bielom),
    /// pri priehladnom pozadi, ked su nepriehladne pixely (kresba) tmave.
    /// </summary>
    public static bool HasDarkInk(Bitmap image)
    {
        ArgumentNullException.ThrowIfNull(image);
        var argb = ReadArgb(image);
        double sum = 0;
        var opaque = 0;
        foreach (var p in argb)
        {
            if (((p >> 24) & 0xFF) < 128)
                continue;
            opaque++;
            sum += (0.299 * ((p >> 16) & 0xFF) + 0.587 * ((p >> 8) & 0xFF) + 0.114 * (p & 0xFF)) / 255;
        }

        if (opaque == 0)
            return false;
        var light = sum / opaque >= 0.5;
        return opaque * 2 >= argb.Length ? light : !light;
    }

    /// <summary>
    /// Priemerne pokrytie stvorca <paramref name="size" /> × <paramref name="size" /> pixelov od <paramref name="x" />,
    /// <paramref name="y" />; pixely mimo obrazka su pozadie.
    /// </summary>
    public float Average(int x, int y, int size)
    {
        float sum = 0;
        for (var dy = 0; dy < size; dy++)
        {
            var py = y + dy;
            if (py < 0 || py >= Height)
                continue;
            for (var dx = 0; dx < size; dx++)
            {
                var px = x + dx;
                if (px >= 0 && px < Width)
                    sum += _values[py * Width + px];
            }
        }

        return sum / (size * size);
    }

    private static int[] ReadArgb(Bitmap image)
    {
        var rect = new Rectangle(0, 0, image.Width, image.Height);
        var data = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        try
        {
            var pixels = new int[image.Width * image.Height];
            // riadky mozu mat zarovnanie (Stride) - kopiruje sa po riadkoch
            for (var y = 0; y < image.Height; y++)
                Marshal.Copy(data.Scan0 + y * data.Stride, pixels, y * image.Width, image.Width);
            return pixels;
        }
        finally
        {
            image.UnlockBits(data);
        }
    }
}
