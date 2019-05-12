using CommandLine;

namespace HasherC
{
    public class Options
    {
        [Option('i', "isinst", Required = false, HelpText = "Specify if distribution is/has installer.")]
        public bool IsInst { get; set; }

        [Option('d', "makedox", Required = false, HelpText = "Specify if protocol documents have to be created.")]
        public bool Makedox { get; set; }

        [Option('p', "path", Required = true, HelpText = "Enter the path to folder/image.")]
        public string Path { get; set; }
    }
}