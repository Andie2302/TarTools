// See https://aka.ms/new-console-template for more information

using System;
using System.Formats.Tar;
using TarTools.Streams;

Console.WriteLine("Hello, World!");
var ms = new MemoryStream();
var tarFileStream = new TarFileStream(ms, true);
tarFileStream.WriteName("test");

File.WriteAllBytes("test.tar", ms.ToArray());
