namespace PoViEmu.Core.Infos
{
    public class Chip
    {
        public string Caption { get; set; }
        public string LoadOffset { get; set; }
        public string ChipSize { get; set; }
        public ChipKind ChipKind { get; set; }
        public string ProgChipName { get; set; }
        public string ProgAreaSize { get; set; }
        public byte ConnectionBit { get; set; }
        public FileRef File { get; set; }
    }
}