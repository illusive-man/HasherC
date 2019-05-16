using System.Collections.Generic;
using System.Runtime.InteropServices;
using CommandLine;
using CommandLine.Text;

namespace HasherC
{
    public class Options
    {
        [Value(0, Hidden = true, Required = true, HelpText = "Path to a directory to calculate hashes for.")]
        public string Path { get; set; }

        [Option('r', "report", Required = false, HelpText = "Report path and filename without extension.")]
        public string Report { get; set; }
    }
}