namespace PoViEmu.Inventory.Infos
{
    public class FileRef
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public byte[] Brotli { get; set; }
    }
}