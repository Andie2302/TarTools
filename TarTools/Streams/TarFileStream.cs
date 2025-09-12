using System;
using System.IO;
using TarTools.Streams.Base;
namespace TarTools.Streams;
public class TarFileStream(Stream? stream, bool leaveOpen)
{
    private readonly BaseTarFileStream _base = new(stream, leaveOpen);
    public void WriteAsciiText(string name, int minimalTextLength = -1, byte filler = 0x00)
    {
        var actualMinimalLength = minimalTextLength <= name.Length ? name.Length : minimalTextLength;
        var array = new byte[actualMinimalLength];
        TarTools.Fill(array, filler, 0, actualMinimalLength);
        var sourceBytes = TarTools.GetAsciiBytes(name);
        Buffer.BlockCopy(sourceBytes, 0, array, 0, Math.Min(sourceBytes.Length, array.Length));
        _base.Write(array);
    }
    public void WriteAsciiDecimalNumber(long number, int minDigits = 0) => WriteFormattedNumber(number, minDigits, "D");
    public void WriteAsciiHexNumber(long number, int minDigits = 0) => WriteFormattedNumber(number, minDigits, "X");
    public void WriteAsciiOctalNumber(long number, int minDigits = 0)
    {
        var octalString = Convert.ToString(number, 8);
        if (minDigits > 0)
            octalString = octalString.PadLeft(minDigits, '0');
        _base.Write(TarTools.GetAsciiBytes(octalString));
    }
    private void WriteFormattedNumber(long number, int minDigits, string format) => _base.Write(TarTools.GetAsciiBytes(number.ToString(minDigits > 0 ? $"{format}{minDigits}" : format)));
}