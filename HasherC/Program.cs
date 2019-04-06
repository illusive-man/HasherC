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
            string[] files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);

            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
            }

            foreach (string file in files)
            {
                var info = new FileInfo(file);
                Console.WriteLine($"Name: { Path.GetFileNameWithoutExtension(file) }, size = {info.Length} bytes");
            }

            Console.ReadKey();
        }
    }
}
