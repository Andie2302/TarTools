using System;
using System.Text;

namespace TarTools;

public static class TarTools
{
    public static byte[] GetAsciiBytes(string text) => Encoding.GetEncoding("ASCII").GetBytes(text);

    public static byte[] Fill(byte[] array, byte b, int start, int length)
    {
        // Validierung der Eingabeparameter
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start), "Start index cannot be negative");
        
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length cannot be negative");
        
        if (start + length > array.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "Start index and length exceed array bounds");
        
        // Füllen des Arrays mit dem angegebenen Byte-Wert
        for (var i = start; i < start + length; i++)
        {
            array[i] = b;
        }

        return array;
    }
}