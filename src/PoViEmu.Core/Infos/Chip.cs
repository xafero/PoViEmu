namespace Discover
{
    public class Chip
    {
        public string Name { get; set; }
        public byte Bus { get; set; }
        public string Code { get; set; }
        public string Offset { get; set; }
        public string ProgArea { get; set; }
        public ChipKind Kind { get; set; }
        public FileRef File { get; set; }
    }
}