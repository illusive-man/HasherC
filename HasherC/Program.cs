using System;
using CommandLine;
using System.IO;
using NLog;

namespace HasherC
{
    internal class Program
    {

        public class Options
        {
            [Option('i', "isinst", Required = false, HelpText = "Specify if distribution is/has installer.")]
            public bool IsInst { get; set; }

            [Option('d', "makedoc", Required = false, HelpText = "Specify if protocol documents have to be created.")]
            public bool Makedox { get; set; }

            [Option('p', "path", Required = true, HelpText = "Enter the path to folder/image.")]
            public string Path { get; set; }
        }


        private static void Main(string[] args)
        {
            string rootDir = @"D:\CRC_TEST";
            var logger = LogManager.GetCurrentClassLogger();
            Parser.Default.ParseArguments<Options>(args)
           .WithParsed(o =>
           {
               if (o.IsInst || o.Makedox || o.Path != null)
               {
                   Console.WriteLine($"Current Arguments: -i {o.IsInst} -d {o.Makedox} -p {o.Path}");
                   rootDir = o.Path;
               }
           });


             
            string[] files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
            string[] dirs = Directory.GetDirectories(rootDir, "*", SearchOption.AllDirectories);
            

            foreach (string file in files)
            {
                var info = new FileInfo(file);
                Console.WriteLine(
                  $"Path: { Path.GetDirectoryName(file) }," +
                  $" Name: { Path.GetFileName(file) }, " +
                  $" Size: { info.Length } bytes");
            }
            var test = ~256 + 1;

            Console.WriteLine($"{ test }");
            logger.Log(LogLevel.Warn, "Sample informational message, called from other context");

            Console.ReadKey();

        }
    }
}
