using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public abstract class SkipArg : OpArg, ICalcArg
    {
        protected SkipArg(bool signed)
        {
            IsSigned = signed;
        }

        public bool IsSigned { get; }

        protected short? GetSkippedShort(byte val)
        {
            var off = (short?)Parent?.Offset;
            var len = (short?)Parent?.Bytes.Length;
            var skip = (short)(IsSigned ? (sbyte)val : val);
            return (short?)(off + len + skip);
        }

        protected short? GetSkippedShort(short val)
        {
            var off = (short?)Parent?.Offset;
            var len = (short?)Parent?.Bytes.Length;
            var skip = (int)(IsSigned ? val : (ushort)val);
            return (short?)(off + len + skip);
        }

        public Instruction? Parent { private get; set; }
    }
}