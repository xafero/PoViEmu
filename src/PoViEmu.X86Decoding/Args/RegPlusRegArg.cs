using PoViEmu.Common;
using PoViEmu.X86Decoding.Ops;

namespace PoViEmu.X86Decoding.Args
{
    public sealed class RegPlusRegArg : OpArg, IByteArg
    {
        public RegPlusRegArg(Register a, Register b, byte? val)
        {
            A = a;
            B = b;
            Val = val;
        }

        public Register A { get; }
        public Register B { get; }
        public byte? Val { get; }

        public override string ToString()
        {
            return $"[{A.ToSmallName()}+{B.ToSmallName()}]";
        }

        public byte[] Bytes => Val == null ? [] : [Val.Value];
    }
}