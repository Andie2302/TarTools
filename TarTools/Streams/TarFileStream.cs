using TarTools.Streams.Base;

namespace TarTools.Streams;

public class TarFileStream(BaseTarFileStream? @base)
{
    private readonly BaseTarFileStream? _base = @base;
}