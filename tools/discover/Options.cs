using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global

namespace Discover
{
    public class Options
    {
        [Option('h', "hex", HelpText = "Find hex matches.")]
        public bool HexMatch { get; set; }

        [Option('l', "line", HelpText = "Find file hex lines.")]
        public bool HexLine { get; set; }

        [Option('r', "rip", HelpText = "Find binary app infos.")]
        public bool ReadBins { get; set; }

        [Option('n', "look", HelpText = "Find file details infos.")]
        public bool LookBins { get; set; }

        [Option('m', "mod", HelpText = "Find device models.")]
        public bool GetModels { get; set; }
        
        [Option('i', "input", HelpText = "Set input directory.")]
        public string InputDir { get; set; }
    }
}