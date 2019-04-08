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

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                Console.WriteLine(value: $"Path: { Path.GetDirectoryName(file) },  Name: { Path.GetFileName(file) }, size = {info.Length} bytes");
            }

            Console.ReadKey();
        }
    }
}
