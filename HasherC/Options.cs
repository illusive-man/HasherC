using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
// ReSharper disable All

namespace HasherC
{
    public class Options
    {
        [Value(0, Hidden = true, Required = true, HelpText = "Path to a directory to calculate hashes for.")]
        public string Path { get; set; }

        [Option('r', "report", Required = false, HelpText = "Reports path and filename without extension.")]
        public string Report { get; set; }

        [Option('m', "mirror", Required = false, HelpText = "Mirror file output to stdout.")]
        public bool Mirror { get; set; }

        [Usage(ApplicationAlias = "")]
        public static IEnumerable<Example> Examples =>
            new List<Example>() {
                new Example("hasherc.exe <path> [options]", new Options {})
            };
    }
}