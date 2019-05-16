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
           })
           .WithNotParsed( errs => { Environment.Exit(1); });

            var files = Directory.GetFiles(_targetPath, "*.*", SearchOption.AllDirectories);
            var outList = new List<string>();

            foreach (var file in files)
            {
                //Can I make cpverify.exe as Embedded Resource in exe file and call it from the code?
                var cpvArgs = $"-mk \"{ file }\"";
                var outData = RunCommand.GetHashes("cpverify.exe", cpvArgs);
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
            
            //outList.Sort();
            Directory.CreateDirectory(Path.GetDirectoryName(_reportPath) ?? throw new InvalidOperationException());

            using (var file = new StreamWriter(_reportPath + ".cpv"))
            {
                foreach (var line in outList)
                {
                    var dataWithRelPath = line.Replace(_targetPath, ".");
                    file.Write(dataWithRelPath);
                    Console.Write(dataWithRelPath);
                }
            }

            Console.WriteLine("\nStarting MAGMA hash calculations...\n");

            var magmaArgs = $"-r \"{ _targetPath }\"";
            var outdataMagma = RunCommand.GetHashes("mgmce.exe", magmaArgs);

            using (var file = new StreamWriter(_reportPath + ".txt"))
            {
                file.Write(outdataMagma);
                Console.WriteLine(outdataMagma);
            }

            Console.WriteLine("\nAll files processed.");
            //logger.Log(LogLevel.Info, "Calculating hashes finished.");

            /*
             * TODO: Add logging events solution-wide
             *
             */
        }
    }
}
