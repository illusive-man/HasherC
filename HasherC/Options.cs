using CommandLine;

namespace HasherC
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Path to a folder to process.")]
        public string Path { get; set; }

        [Option('r', "report", Required = false, HelpText = "Path to a file to save the report to.")]
        public string Report { get; set; }

        [Option('x', "exclude", Required = false, HelpText = "Folder to exclude from processing.")]
        public string Exclude { get; set; }
    }
}