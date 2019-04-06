using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HasherC
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootDir = @"D:\CRC_TEST";

            string[] dirs = Directory.GetDirectories(rootDir, "*", SearchOption.AllDirectories);

            foreach (string dir in dirs)
            {
                Console.WriteLine($"{dir}");
            }

            Console.ReadKey();
        }
    }
}
