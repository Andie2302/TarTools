using System.Text;

namespace TarTools;

public static class TarTools
{
    public static byte[] GetAsciiBytes(string text) => Encoding.GetEncoding("ASCII").GetBytes(text);
}