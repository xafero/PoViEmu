namespace PoViEmu.Core.Risc
{
    public static class InstTool
    {
        public static Instruction Create(byte first, byte second, OpCodes code,
            byte? i = null, ushort? d = null, byte? n = null, byte? m = null)
        {
            byte[] bytes = [first, second];
            return new Instruction(bytes, code, imm: i, dis: d, dst: n, src: m);
        }

        public static (byte high, byte low) SplitByte(byte val)
        {
            var high = val >> 4;
            var low = val & 0b00001111;
            return ((byte)high, (byte)low);
        }

        public static ushort CombineBytes(byte first, byte second)
        {
            var res = (first << 8) | second;
            return (ushort)res;
        }
    }
}