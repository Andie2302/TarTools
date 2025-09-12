using System;
using System.Text;
namespace TarTools;
public static class TarTools
{
    public static byte[] GetAsciiBytes(string text) => Encoding.GetEncoding("ASCII").GetBytes(text);
    
    /// <summary>
    /// Füllt einen Bereich des Arrays mit dem angegebenen Byte-Wert.
    /// </summary>
    /// <param name="array">Das zu füllende Array</param>
    /// <param name="value">Der Byte-Wert zum Füllen</param>
    /// <param name="start">Startindex</param>
    /// <param name="length">Anzahl der zu füllenden Bytes</param>
    /// <returns>Das gefüllte Array (für Method Chaining)</returns>
    public static byte[] Fill(byte[] array, byte value, int start, int length)
    {
        ValidateFillParameters(array, start, length);
        
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        array.AsSpan(start, length).Fill(value);
#else
        for (var i = start; i < start + length; i++)
        {
            array[i] = value;
        }
#endif
        return array;
    }
    
    /// <summary>
    /// Füllt das gesamte Array mit dem angegebenen Byte-Wert.
    /// </summary>
    /// <param name="array">Das zu füllende Array</param>
    /// <param name="value">Der Byte-Wert zum Füllen</param>
    /// <returns>Das gefüllte Array (für Method Chaining)</returns>
    public static byte[] Fill(byte[] array, byte value)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        
        return Fill(array, value, 0, array.Length);
    }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Füllt einen Span mit dem angegebenen Byte-Wert.
    /// </summary>
    /// <param name="span">Der zu füllende Span</param>
    /// <param name="value">Der Byte-Wert zum Füllen</param>
    public static void Fill(Span<byte> span, byte value)
    {
        span.Fill(value);
    }
#endif

    private static void ValidateFillParameters(byte[] array, int start, int length)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start), "Start index cannot be negative");
        
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length cannot be negative");
        
        if (start + length > array.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "Start index and length exceed array bounds");
    }
}