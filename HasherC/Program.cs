using System;
using System.Collections.Generic;
using CommandLine;
using System.IO;
using System.Linq;

// ReSharper disable All

namespace HasherC
{
    internal static class Program
    {
        private static string _targetPath;
        private static string _reportPath;
        private static bool _mirror = false;

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
                   _reportPath = Path.Combine(_targetPath, "report");
               }

               _mirror = o.Mirror;
           })
           .WithNotParsed( errs => { Environment.Exit(1); });

            Directory.CreateDirectory(Path.GetDirectoryName(_reportPath) ?? throw new InvalidOperationException());

            var list = Directory.GetFiles(_targetPath, "*.*", SearchOption.AllDirectories);
            var files = list.OrderBy(f => f);
            var outList = new List<string>();
            
            Console.WriteLine($"\nStarting hash calculations on directory: {_targetPath}");
            if(_mirror)
                Console.WriteLine("===== CPVERIFY =====");

            
            using (var progress = new ProgressBar())
            {
                var iteration = 0;
                var step = 100 / files.Count() + 1;

                foreach (var file in files)
                {
                   
                    var cpvArgs = $"-mk \"{file}\"";
                    var outData = RunCommand.GetHashes("cpverify.exe", cpvArgs);

                    if (!string.IsNullOrEmpty(outData))
                    {
                        outList.Add($"{file}\t{outData}");
                    }
                    else
                    {
                        var filler = new string('0', 64);
                        outList.Add($"{file}\t{filler}\n");
                    }

                    progress.Report((double) iteration/100) ;
                    iteration += step;
                }
            }
            
            using (var file = new StreamWriter(_reportPath + ".cpv"))
            {
                foreach (var line in outList)
                {
                    var dataWithRelPath = line.Replace(_targetPath, ".");
                    file.Write(dataWithRelPath);
                    if(_mirror)
                        Console.Write(dataWithRelPath);
                }
            }

            if(_mirror) Console.WriteLine("====== MAGMA ======");

            var magmaArgs = $"-r \"{ _targetPath }\"";
            var outdataMagma = RunCommand.GetHashes("mgmce.exe", magmaArgs);

            using (var file = new StreamWriter(_reportPath + ".txt"))
            {
                file.Write(outdataMagma);
                if(_mirror)
                    Console.WriteLine(outdataMagma);
            }

            Console.WriteLine("Hash calculations were finished successfully!");
            Console.WriteLine($"Reports created: (cpverify) -> {_reportPath}.cpv, (magmac) -> {_reportPath}.txt\n");
            
            //logger.Log(LogLevel.Info, "Calculating hashes finished.");
        }
    }
}
