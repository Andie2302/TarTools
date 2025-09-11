using System;
using System.Threading;
using System.Threading.Tasks;

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
namespace TarTools.Streams.Base
{
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
                        await _stream.DisposeAsync();
                }
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        // Memory<T> Support für async Operationen
        public ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            return _stream.ReadAsync(buffer, cancellationToken);
        }

        public ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            return _stream.WriteAsync(buffer, cancellationToken);
        }
    }
}
#endif