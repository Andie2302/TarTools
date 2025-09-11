using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TarTools.Streams.Base;

public partial class BaseTarFileStream(Stream? stream, bool leaveStreamOpen = false) : IDisposable
{
    private readonly Stream _stream = stream ?? throw new ArgumentNullException(nameof(stream));
    private bool _disposed;

    
    // Dispose Pattern
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed || !disposing) return;
        if (!leaveStreamOpen) 
            _stream?.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

   

    
    // Disposed Check
    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(BaseTarFileStream));
    }

    
    // Basic Stream Operations
    public void Flush()
    {
        ThrowIfDisposed();
        _stream.Flush();
    }

    public void Write(byte[] buffer, int offset, int count)
    {
        ThrowIfDisposed();
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (offset + count > buffer.Length) throw new ArgumentException("Invalid offset and count combination.");
        
        _stream.Write(buffer, offset, count);
    }

    public int Read(byte[] buffer, int offset, int count)
    {
        ThrowIfDisposed();
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (offset + count > buffer.Length) throw new ArgumentException("Invalid offset and count combination.");
        
        return _stream.Read(buffer, offset, count);
    }

    
    // Stream Properties
    public bool CanRead 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.CanRead; 
        } 
    }
    
    public bool CanWrite 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.CanWrite; 
        } 
    }
    
    public bool CanSeek 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.CanSeek; 
        } 
    }
    
    public bool CanTimeout 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.CanTimeout; 
        } 
    }

    
    // Position and Length Operations
    public long Position 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.Position; 
        }
        set
        {
            ThrowIfDisposed();
            _stream.Position = value;
        }
    }
    
    public long Length 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.Length; 
        } 
    }
    
    public long Seek(long offset, SeekOrigin origin)
    {
        ThrowIfDisposed();
        return _stream.Seek(offset, origin);
    }
    
    public void SetLength(long value)
    {
        ThrowIfDisposed();
        _stream.SetLength(value);
    }

    
    // Asynchronous Methods
    public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (offset + count > buffer.Length) throw new ArgumentException("Invalid offset and count combination.");
        
        return _stream.WriteAsync(buffer, offset, count, cancellationToken);
    }

    public Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (offset + count > buffer.Length) throw new ArgumentException("Invalid offset and count combination.");
        
        return _stream.ReadAsync(buffer, offset, count, cancellationToken);
    }

    public Task FlushAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        return _stream.FlushAsync(cancellationToken);
    }

    
    // Convenience Methods
    public void Write(byte[] buffer)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        Write(buffer, 0, buffer.Length);
    }

    public byte[] ReadExact(int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (count == 0) return [];

        var buffer = new byte[count];
        var totalRead = 0;

        while (totalRead < count)
        {
            var bytesRead = Read(buffer, totalRead, count - totalRead);
            if (bytesRead == 0)
                throw new EndOfStreamException($"Unexpected end of stream. Expected {count} bytes, got {totalRead}.");
            
            totalRead += bytesRead;
        }

        return buffer;
    }

    public async Task<byte[]> ReadExactAsync(int count, CancellationToken cancellationToken = default)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (count == 0) return [];

        var buffer = new byte[count];
        var totalRead = 0;

        while (totalRead < count)
        {
            var bytesRead = await ReadAsync(buffer, totalRead, count - totalRead, cancellationToken);
            if (bytesRead == 0)
                throw new EndOfStreamException($"Unexpected end of stream. Expected {count} bytes, got {totalRead}.");
            
            totalRead += bytesRead;
        }

        return buffer;
    }

    public Task WriteAsync(byte[] buffer, CancellationToken cancellationToken = default)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        return WriteAsync(buffer, 0, buffer.Length, cancellationToken);
    }

    
    // Timeout Properties (falls der underlying Stream sie unterstützt)
    public int ReadTimeout 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.ReadTimeout; 
        }
        set
        {
            ThrowIfDisposed();
            _stream.ReadTimeout = value;
        }
    }
    
    public int WriteTimeout 
    { 
        get 
        { 
            ThrowIfDisposed(); 
            return _stream.WriteTimeout; 
        }
        set
        {
            ThrowIfDisposed();
            _stream.WriteTimeout = value;
        }
    }
}