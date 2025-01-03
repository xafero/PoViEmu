namespace PoViEmu.Base.CPU
{
    public static class BitTool
    {
        public static byte GetHigh(ushort value)
            => (byte)((value >> 8) & 0xFF);

        public static ushort SetHigh(ushort value, byte high)
            => (ushort)((value & 0x00FF) | (high << 8));

        public static byte GetLow(ushort value)
            => (byte)(value & 0xFF);

        public static ushort SetLow(ushort value, byte low)
            => (ushort)((value & 0xFF00) | low);
        
        public static (ushort first, ushort second) Split(this uint val)
        {
            var first = (ushort)(val & 0xFFFF);
            var second = (ushort)((val >> 16) & 0xFFFF);
            return (first, second);
        }
    }
}