// See https://aka.ms/new-console-template for more information

using TarTools.Streams;

Console.WriteLine("Hello, World!");
var ms = new MemoryStream();
var tarFileStream = new TarFileStream(ms, true);
tarFileStream.WriteAsciiText("decimal:");
tarFileStream.WriteAsciiDecimalNumber(1000);
tarFileStream.WriteAsciiText("octal:");
tarFileStream.WriteAsciiOctalNumber(1000);
tarFileStream.WriteAsciiText("hex:");
tarFileStream.WriteAsciiHexNumber(1000);

File.WriteAllBytes("test.tar", ms.ToArray());
