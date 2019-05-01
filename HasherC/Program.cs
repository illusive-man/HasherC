using System;
using CommandLine;
using System.IO;
using NLog;

namespace HasherC
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string rootDir;
            rootDir = @"D:\CRC_TEST";
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
             
            var files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
            var dirs = Directory.GetDirectories(rootDir, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var info = new FileInfo(file);
                Console.WriteLine(
                  $"Path: { Path.GetDirectoryName(file) }," +
                  $" Name: { Path.GetFileName(file) }, " +
                  $" Size: { info.Length } bytes");
            }
            var test = new string[2] {"Test", "Case"} ;


            Console.WriteLine($"{ test[0] }");
            logger.Log(LogLevel.Warn, "Sample informational message, called from other context");

            Console.ReadKey();

        }
    }
}
