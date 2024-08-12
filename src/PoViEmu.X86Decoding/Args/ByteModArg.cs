using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ByteModArg : OpArg, IByteArg
    {
        public ByteModArg(byte val)
        {
            Val = val;
        }

        public byte Val { get; }

        public override string ToString()
        {
            var val = Val.HaveComplement(out var isNeg);
            var op = isNeg ? '-' : '+';
            return $"byte {op}0x{val:x}";
        }

        public byte[] Bytes => [Val];
    }
}