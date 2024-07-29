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

        public override string ToString()
        {
            var off = Parent?.Offset;
            var len = Parent?.Bytes.Length;
            var skip = (int)(IsSigned ? (sbyte)Value : Value);
            var skippedAddr = off + len + skip;
            return $"0x{skippedAddr:x}";
        }

        public Instruction Parent { get; set; }
    }
}