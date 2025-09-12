// See https://aka.ms/new-console-template for more information

using TarTools.Streams;

Console.WriteLine("Hello, World!");
var ms = new MemoryStream();
var tarFileStream = new TarFileStream(ms, true);
tarFileStream.WriteName("abcde");

File.WriteAllBytes("test.tar", ms.ToArray());
