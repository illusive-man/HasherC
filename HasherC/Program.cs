using System;
using CommandLine;
using System.IO;
using NLog;

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
    
        public class MyClass
        {
            private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

            public void MyMethod1()
            {
                logger.Trace("Sample trace message");
                logger.Debug("Sample debug message");
                logger.Info("Sample informational message");
                logger.Warn("Sample warning message");
                logger.Error("Sample error message");
                logger.Fatal("Sample fatal error message");

                // alternatively you can call the Log() method
                // and pass log level as the parameter.
                logger.Log(LogLevel.Info, "Sample informational message");


                // Example of logging an exception
                try
                {

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "ow noos!"); // render the exception with ${exception}
                    throw;
                }


            }
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
            int test = ~256 + 1;

            Console.WriteLine($"{ test }");

            var etc = new MyClass();
            etc.MyMethod1();

            try
            {
                var bbb = 79228162514264337593543950335M;
                byte a = 0b11111101;
                byte a1 = 253;
                short b = 0b11111101;
                Console.WriteLine($"Byte a: {a}; byte a1: {a1}; short a: { (sbyte)a}; short a1: {(sbyte)a1}");
                //byte c = checked((decimal)(a + b));
                //Console.WriteLine(c);
                Console.WriteLine($"a={a}, b={b}");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }

            

            Console.ReadKey();
        }
    }
}
