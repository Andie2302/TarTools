using System;
using System.IO;
using TarTools.Streams.Base;

namespace TarTools.Streams;

public class TarFileStream
{
    private readonly BaseTarFileStream _base;
    public TarFileStream(Stream? stream, bool leaveOpen) => _base = new BaseTarFileStream(stream, leaveOpen);

    public void WriteName(string name, int size = 100, byte filler = 0x00)
    {
        var array = new byte[size];
        TarTools.Fill(array, filler, 0, size);
        var quelle = TarTools.GetAsciiBytes(name);
        Buffer.BlockCopy(quelle, 0, array, 0, Math.Min(quelle.Length, array.Length));
        _base.Write(array);
    }
}