namespace ElenFontEditor.Tools;

public static class Utils
{
    public static byte ConvertBoolArrayToByte(bool[] source)
    {
        byte result = 0;
        var index = 8 - source.Length;

        foreach (var b in source)
        {
            // if the element is 'true' set the bit at that position
            if (b)
                result |= (byte)(1 << (7 - index));
            index++;
        }

        return result;
    }

    public static bool[] ConvertByteToBoolArray(byte b)
    {
        var result = new bool[8];
        // check each bit in the byte. if 1 set to true, if 0 set to false
        for (var i = 0; i < 8; i++)
            result[i] = (b & (1 << i)) != 0;

        Array.Reverse(result);

        return result;
    }
}