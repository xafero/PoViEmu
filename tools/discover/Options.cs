using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global

namespace Discover
{
    public class Options
    {
        [Option('h', "hex", HelpText = "Find hex matches.")]
        public bool HexMatch { get; set; }

        [Option('r', "rip", HelpText = "Find binary app infos.")]
        public bool ReadBins { get; set; }
        
        [Option('p', "isa", HelpText = "Find ISA in files.")]
        public bool PullIsa { get; set; }

        [Option('i', "input", HelpText = "Set input directory.")]
        public string InputDir { get; set; }
    }
}