using System.IO;
using TarTools.Streams.Base;

namespace TarTools.Streams;

public class TarFileStream
{
    private readonly BaseTarFileStream _base;
    public TarFileStream(Stream? stream, bool leaveOpen) => _base = new BaseTarFileStream(stream, leaveOpen);
    public void WriteName(string name) => _base.Write(TarTools.GetAsciiBytes(name));
}