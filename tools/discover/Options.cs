using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global

namespace Discover
{
    public class Options
    {
        [Option('h', "hex", HelpText = "Find hex matches.")]
        public bool HexMatch { get; set; }

        [Option('i', "input", HelpText = "Set input directory.")]
        public string InputDir { get; set; }
    }
}