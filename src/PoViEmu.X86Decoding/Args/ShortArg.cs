using System;
using PoViEmu.Common;
using PoViEmu.X86Decoding.Ops;

namespace PoViEmu.X86Decoding.Args
{
    public sealed class ShortArg : OpArg, IByteArg
    {
        public ShortArg(short val, bool signed)
        {
            Value = val;
            if (signed && val.HaveComplement(out var isNeg) is var signedVal && isNeg)
                SignedVal = signedVal;
        }

        public short Value { get; }
        public short? SignedVal { get; }

        public override string ToString()
        {
            var val = SignedVal ?? Value;
            return $"0x{val:x}";
        }

        public byte[] Bytes => BitConverter.GetBytes(Value);
    }
}