using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ByteArg : OpArg, IByteArg
    {
        public ByteArg(byte val, bool signed)
        {
            Value = val;
            if (signed && val.HaveComplement(out var isNeg) is var signedVal && isNeg)
                SignedVal = signedVal;
        }

        public byte Value { get; }
        public byte? SignedVal { get; }

        public override string ToString()
        {
            var val = SignedVal ?? Value;
            return $"0x{val:x}";
        }

        public byte[] Bytes => [Value];
    }
}