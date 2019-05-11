using System;
using System.Diagnostics;
using CommandLine;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using NLog;

namespace HasherC
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var rootDir = @"d:\CRC_TEST";
            var logger = LogManager.GetCurrentClassLogger();
            Parser.Default.ParseArguments<Options>(args)
           .WithParsed(o =>
           {
               if (o.IsInst || o.Makedox || o.Path != null)
               {
                   Console.WriteLine($"Current Arguments: -i {o.IsInst} -d {o.Makedox} -p {o.Path}");
                   rootDir = o.Path;
                   
                   logger.Log(LogLevel.Info, "Options are set, trying to parse.");
               }
           });

            var files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
            var dirs = Directory.GetDirectories(rootDir, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var info = new FileInfo(file);
                Console.WriteLine($"Path: {Path.GetDirectoryName(file)}, Name: {Path.GetFileName(file)}, Size: {info.Length} bytes");
            }


            // TODO: Add logging events solution-wide
            //logger.Log(LogLevel.Warn, "Sample informational message, called from other context");

            var command = new RunCommand();

        }
    }
}
