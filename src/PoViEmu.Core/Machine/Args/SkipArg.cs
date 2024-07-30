using PoViEmu.Core.Machine.Core;

namespace PoViEmu.Core.Machine.Args
{
    public class SkipArg : ConstantArg, ICalcArg
    {
        public bool IsSigned { get; set; }

        public SkipArg(byte value, bool signed = false) : base(value)
        {
            IsSigned = signed;
        }

        public short? GetSkippedAddr()
        {
            var off = (short?)Parent?.Offset;
            var len = (short?)Parent?.Bytes.Length;
            var skip = (short)(IsSigned ? (sbyte)Value : Value);
            return (short?)(off + len + skip);
        }

        public override string ToString()
        {
            var skippedAddr = GetSkippedAddr();
            return $"0x{skippedAddr:x}";
        }

        public Instruction Parent { get; set; }
    }
}