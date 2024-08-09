using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class SkipArg : OpArg, ICalcArg
    {
        public SkipArg(byte? value, bool signed = true)
        {
            IsSigned = signed;
            Value = value.GetValueOrDefault();
        }

        public bool IsSigned { get; }
        public byte Value { get; }

        public short? GetSkippedAddr()
        {
            var off = (short?)Parent?.Offset;
            var len = (short?)Parent?.Bytes.Length;
            var skip = (short)(IsSigned ? (sbyte)Value : Value);
            return (short?)(off + len + skip);
        }

        public Instruction? Parent { private get; set; }

        public override string ToString()
        {
            var skippedAddr = GetSkippedAddr();
            return $"0x{skippedAddr:x}";
        }
    }
}