// ReSharper disable ClassNeverInstantiated.Global

namespace PoViEmu.Inventory.Upper
{
    public class ConstFile
    {
        public string? Name { get; set; }

        public string? Sha256 { get; set; }

        public string? Brotli { get; set; }
    }

    public class Settings
    {
        public string? Erased { get; set; }
    }

    public class FilePart
    {
        public string? Name { get; set; }

        public int? Offset { get; set; }

        public int? Length { get; set; }
    }

    public class Bunk
    {
        public string? Start { get; set; }

        public FilePart? File { get; set; }
    }

    public class SeedEntry
    {
        public string? Base { get; set; }

        public string[]? Comments { get; set; }

        public ConstFile[]? Files { get; set; }

        public Settings? Settings { get; set; }

        public Bunk[]? Bunks { get; set; }
    }
}