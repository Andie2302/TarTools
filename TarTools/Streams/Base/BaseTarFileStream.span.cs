using System;

namespace TarTools.Streams.Base;

public partial class BaseTarFileStream
{
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    public int Read(Span<byte> buffer)
    {
        ThrowIfDisposed();
        return _stream.Read(buffer);
    }

    public void Write(ReadOnlySpan<byte> buffer)
    {
        ThrowIfDisposed();
        _stream.Write(buffer);
    }
#endif
}