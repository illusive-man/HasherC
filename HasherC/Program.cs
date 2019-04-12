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

            [Option('p', "path", Required = true, HelpText = "Enter the path.")]
            public string Path { get; set; }
        }
        
        static void Main(string[] args)
        {
         
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       if (o.Verbose || o.Check)
                       {
                           Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose} -c {o.Check} -p {o.Path}");
                           Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                       }
                   });


            string rootDir = @"D:\CRC_TEST";
 
            string[] dirs = Directory.GetDirectories(rootDir, "*", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                //Console.WriteLine(
                  //  $"Path: { Path.GetDirectoryName(file) }," +
                  //  $" Name: { Path.GetFileName(file) }, " +
                  //  $" Size: { info.Length } bytes");
            }
            int test = 6 / 2 * (1 + 2);
            Console.WriteLine($"{ test }");
            Console.ReadKey();
        }
    }
}
