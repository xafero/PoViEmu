namespace PoViEmu.Core.Risc
{
    public sealed class Instruction
    {
        public Instruction(byte[] bytes, OpCodes code,
            byte? imm = null, ushort? dis = null,
            byte? dst = null, byte? src = null)
        {
            Bytes = bytes;
            Code = code;
            Immediate = imm;
            Displaced = dis;
            DestReg = dst;
            SrcReg = src;
        }

        public OpCodes Code { get; init; }
        public byte[] Bytes { get; init; }
        public byte? Immediate { get; init; }
        public ushort? Displaced { get; init; }
        public byte? DestReg { get; init; }
        public byte? SrcReg { get; init; }

        public override string ToString()
        {
            var codeTxt = Code.ToString().ToLower();
            var arg = $"{Immediate:X} {Displaced:X} {DestReg:X} {SrcReg:X}";
            return $"{codeTxt}    {arg}".Trim();
        }
    }
}