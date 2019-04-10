using System;
using CommandLine;
using System.IO;

namespace HasherC
{
    class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            [Option('c', "check", Required = false, HelpText = "Check files in directory.")]
            public bool Check { get; set; }

            [Option('p', "path", Required = false, HelpText = "Check files in directory.")]
            public string Path { get; set; }
        }
        
        static void Main(string[] args)
        {
            Int64 memoryBefore = GC.GetTotalMemory(true);
            Int64 totalMemory = GC.GetTotalMemory(false);

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.Verbose)
                       {
                           Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                           Console.WriteLine("Quick Start Example! App is in Verbose mode!");

                       }
                       else if (o.Check)
                       {
                           Console.WriteLine($"Checking Files. Current Arguments: -c {o.Check}");
                           Console.WriteLine("App is in Check mode!");
                       }
                       else
                       {
                           Console.WriteLine($"Current Arguments: -v {o.Verbose} -c {o.Check}");
                       }
                   });


            string rootDir = @"D:\CRC_TEST";
 
            string[] dirs = Directory.GetDirectories(rootDir, "*", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                Console.WriteLine(
                    $"Path: { Path.GetDirectoryName(file) }," +
                    $" Name: { Path.GetFileName(file) }, " +
                    $" Size: { info.Length } bytes");
            }

            Console.WriteLine($"{ memoryBefore} : { totalMemory }");
            Console.ReadKey();
        }
    }
}
