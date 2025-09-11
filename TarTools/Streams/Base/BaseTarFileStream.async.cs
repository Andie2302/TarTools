using System;
using System.Threading.Tasks;

namespace TarTools.Streams.Base;


#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
public partial class BaseTarFileStream : IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            if (!leaveStreamOpen)
            {
                if (_stream is IAsyncDisposable asyncDisposable)
                    await asyncDisposable.DisposeAsync();
                else
                    _stream.Dispose();
            }
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}
#endif