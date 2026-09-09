using System.Text;
using ElenFontEditor.Entities;

namespace ElenFontEditor.Tools;

internal static class ElenBinParser
{
    public static FontsFile LoadFile(string pathname)
    {
        if (string.IsNullOrEmpty(pathname))
            throw new ArgumentNullException(nameof(pathname));
        if (!File.Exists(pathname))
            throw new FileNotFoundException($"File {pathname} was not found.");

        var file = new FontsFile();
        using var reader = new BinaryReader(File.OpenRead(pathname));

        var positions = new ushort[16];
        for (var i = 0; i < positions.Length; i++)
        {
            positions[i] = reader.ReadUInt16BigEndian();
        }

        var fileName = ReadText(reader.ReadBytes(32));
        var namewe = Path.GetFileNameWithoutExtension(pathname);
        file.Name = fileName;
        file.PathName = pathname;
        if (fileName != namewe && GlobData.Config.StrictMode)
            throw new FormatException($"File {pathname} have different name ({namewe}) than the one defined inside the file: {fileName}.");

        for (var i = 0; i < positions.Length; i++)
        {
            if (positions[i] == 64 && (i != 0 || positions[1] == 64))
                break;

            reader.BaseStream.Position = positions[i];
            var fontname = ReadText(reader.ReadBytes(20));

            if (file.Fonts.Any(f => f.Name == fontname)) 
                continue;

            var font = new TableFont
            {
                Name = fontname,
                MaxWidth = reader.ReadByte(),
                Height = reader.ReadByte(),
                IsProportional = reader.ReadBoolean()
            };

            for (var j = 0; j < 256; j++)
            {
                var width = reader.ReadByte();
                if (!font.IsProportional && width != font.MaxWidth && width != 0 && GlobData.Config.StrictMode)
                    throw new FormatException($"Font {font.Name} is not proportional but index symbol {j} has different width ({width}).");
                if (width > font.MaxWidth && GlobData.Config.StrictMode)
                    throw new FormatException($"Font {font.Name} have max width {font.MaxWidth} but index symbol {j} has bigger width ({width}).");

                var symbol = new FontSymbol(j, width, font);

                for (var r = 0; r < symbol.Rows; r++)
                {
                    for (var c = 0; c < symbol.Columns; c++)
                    {
                        var value = reader.ReadByte();
                        symbol[r,c] = value;
                    }
                }

                font.Symbols[j] = symbol;
            }
            file.Fonts.Add(font);
        }

        return file;
    }

    public static void SaveFile(FontsFile file)
    {
        ArgumentNullException.ThrowIfNull(file);
        if (file.Fonts.Count > 16)
            throw new FormatException("Count of fonts must be smaller than 17 (max is 16).");

        using var writer = new BinaryWriter(File.OpenWrite(file.PathName));

        for (var i = 0; i < 16; i++) 
            writer.WriteBigEndian(64);

        writer.Write(WriteText(file.Name, 32));

        var positions = new ushort[file.Fonts.Count];
        var j = 0;

        foreach (var font in file.Fonts)
        {
            positions[j++] = (ushort) writer.BaseStream.Position;
            writer.Write(WriteText(font.Name, 20));
            writer.Write((byte)font.MaxWidth);
            writer.Write((byte)font.Height);
            writer.Write(font.IsProportional);

            foreach (var symbol in font.Symbols)
            {
                writer.Write((byte)symbol.Width);

                for (var r = 0; r < symbol.Rows; r++)
                    for (var c = 0; c < symbol.Columns; c++)
                        writer.Write(symbol[r, c]);
            }
        }

        writer.Write((byte) 97); // 'a' - checksum?

        writer.BaseStream.Position = 0;

        foreach (var pos in positions)
        {
            writer.WriteBigEndian(pos);
        }
    }

    private static string ReadText(byte[] array)
    {
        ArgumentNullException.ThrowIfNull(array);

        var nameArr = new byte[array.Length];
        Array.Copy(array, nameArr, array.Length);
        var end = false;
        for (var i = 0; i < array.Length; i++)
        {
            if (!end && array[i] == 0) 
                end = true;
            if (end) 
                array[i] = 32;
        }

        return Encoding.ASCII.GetString(array).TrimEnd();
    }

    private static byte[] WriteText(string text, int size)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException(nameof(text));
        if (text.Length > size)
            throw new ArgumentException($"String length must be shorter than {size}.");

        var array = new byte[size];
        var bytes = Encoding.ASCII.GetBytes(text);
        for (var i = 0; i < bytes.Length; i++) 
            array[i] = bytes[i];
        return array;
    }

    private static ushort ReadUInt16BigEndian(this BinaryReader reader) => (ushort) ((reader.ReadByte() << 8) + reader.ReadByte());

    private static void WriteBigEndian(this BinaryWriter writer, ushort value)
    {
        writer.Write((byte)(( value >> 8 ) & 0xFF));
        writer.Write((byte)(value & 0xFF));
    }
}