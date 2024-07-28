using PoViEmu.Core.Machine.Core;

namespace PoViEmu.Core.Machine.Args
{
    public class SkipArg : ConstantArg, ICalcArg
    {
        public SkipArg(byte value) : base(value)
        {
        }

        public override string ToString()
        {
            var off = Parent?.Offset;
            var len = Parent?.Bytes.Length;
            var skip = (int)Value;
            var skippedAddr = off + len + skip;
            return $"0x{skippedAddr:x}";
        }

        public Instruction Parent { get; set; }
    }
}