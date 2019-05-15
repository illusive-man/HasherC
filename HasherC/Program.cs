using System;
using System.Collections.Generic;
using CommandLine;
using System.IO;

// ReSharper disable All

namespace HasherC
{
    internal static class Program
    {
        private static string _targetPath;
        private static string _reportPath;
        private static string _excludePath = null;

        public static void Main(string[] args)
        {
            //var logger = LogManager.GetCurrentClassLogger();

            Parser.Default.ParseArguments<Options>(args)
           .WithParsed(o =>
           {
               if (o.Path != null)
               {
                   _targetPath = o.Path.TrimEnd(Path.DirectorySeparatorChar);
               }

               if (o.Report != null)
               {
                   _reportPath = o.Report;
               }
               else
               {
                   _reportPath = Path.Combine(_targetPath ,"report");
               }

               if (o.Exclude != null)
               {
                   _excludePath = o.Exclude;
               }

           })
           .WithNotParsed( errs => { Environment.Exit(1); });

            var files = Directory.GetFiles(_targetPath, "*.*", SearchOption.AllDirectories);
            var outList = new List<string>();

            foreach (var file in files)
            {
                //Can I make cpverify.exe as Embedded Resource in exe file and call it from the code?
                if (_excludePath != null &&
                    Path.GetDirectoryName(file)
                        .Contains(_excludePath.TrimEnd(Path.DirectorySeparatorChar)))
                    continue;
                var currArgs = $"-mk \"{ file }\"";
                var outData = CpvCommand.GetHashes("cpverify.exe", currArgs);
                if (!string.IsNullOrWhiteSpace(outData))
                {
                    outList.Add($"{ file }\t{ outData }");
                }
                else
                {
                    var filler = new String('0', 64);
                    outList.Add($"{ file }\t{ filler }\r");
                }
            }
            
            outList.Sort();
            Directory.CreateDirectory(Path.GetDirectoryName(_reportPath) ?? throw new InvalidOperationException());

            using (var file = new StreamWriter(_reportPath + ".cpv"))
            {
                foreach (var line in outList)
                {
                    var relPath = line.Replace(_targetPath, ".");
                    file.Write(relPath);
                    Console.WriteLine(relPath);
                }
            }

            Console.WriteLine("\nStarting MAGMA hash calculations...\n");


            var currArgs2 = $"-r \"{ _targetPath }\"";
            var outData2 = CpvCommand.GetHashes("mgmce.exe", currArgs2);

            using (var file = new StreamWriter(_reportPath + ".txt"))
            {
                file.Write(outData2);
                Console.WriteLine(outData2);
            }

            Console.WriteLine("\nAll files processed.");
            //logger.Log(LogLevel.Info, "Calculating hashes finished.");

            /*
             * TODO: Add logging events solution-wide
             * TODO: Embed cpverify?
             */
        }
    }
}
