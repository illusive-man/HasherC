using System;
using System.Collections.Generic;
using CommandLine;
using System.IO;
using System.Reflection;
using NLog;
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
                   _targetPath = o.Path;
               }

               if (o.Report != null)
               {
                   _reportPath = o.Report;
               }
               else
               {
                   _reportPath = Path.Combine(_targetPath, @"report.cpv");
               }

               if (o.Exclude != null)
               {
                   _excludePath = o.Exclude;
               }

           })
           .WithNotParsed( o => { Environment.Exit(1); });

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
                    outList.Add($"{file}\t{outData}");
                }
                else
                {
                    outList.Add($"{file}\t{new String('0', 64)}");
                }
            }
            
            outList.Sort();
            Directory.CreateDirectory(Path.GetDirectoryName(_reportPath) ?? throw new InvalidOperationException());

            using (var file = new StreamWriter(_reportPath))
            {
                foreach (var line in outList)
                {
                    var relPath = line.Replace(_targetPath, ".\\");
                    file.Write(relPath);
                    Console.WriteLine(relPath);
                }
            }

            Console.WriteLine("\nAll files are processed. Check {0} file for report.\n", _reportPath);
            //logger.Log(LogLevel.Info, "Calculating hashes finished.");

            /*
             * TODO: Add parameter to point to cpverify.exe file
             * TODO: Add logging events solution-wide
             * TODO: Try to use only List (including sort testing)
             * TODO: Refactoring methods according hasher names
             */
        }
    }
}
