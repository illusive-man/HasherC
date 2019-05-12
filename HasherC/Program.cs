using System;
using System.Collections.Generic;
using CommandLine;
using System.IO;
using NLog;

namespace HasherC
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var rootDir = @"D:\CRC";
            var logger = LogManager.GetCurrentClassLogger();
            Parser.Default.ParseArguments<Options>(args)
           .WithParsed(o =>
           {
               if (!Directory.Exists(o.Path) || string.IsNullOrWhiteSpace(o.Path)) return;
               Console.WriteLine($"Current Arguments: -i {o.IsInst} -d {o.Makedox} -p {o.Path}");
               rootDir = o.Path;
               logger.Log(LogLevel.Info, "Options are set, trying to parse.");
           });

            var files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
            var resultTable = new SortedDictionary<string, string>();
            var outList = new List<string>();

            foreach (var file in files)
            {
                var currFile = $"-mk \"{file}\"";
                var outData = RunCommand.Run(@".\cpverify.exe", currFile);
                resultTable.Add(file, outData);
            }
            
            foreach (var item in resultTable)
            {
                outList.Add( $"{ item.Key }\t{ item.Value }");
            }

            using (StreamWriter file = new StreamWriter(@"D:\CRC\Hashes.cpv"))
            {
                foreach (string line in outList)
                {
                    file.Write(line);
                    Console.WriteLine(line);
                }
            }

            /*
             * TODO: Add options to process a single file (detect if file or folder sent in -p parameter's argument)
             * TODO: Add logging events solution-wide
             * TODO: Try to use only List (including sort testing)
             * TODO: Refactoring methods according hasher names
             * logger.Log(LogLevel.Warn, "Sample informational message, called from other context");
             */
        }
    }
}
