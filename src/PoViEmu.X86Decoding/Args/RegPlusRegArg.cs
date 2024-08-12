using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
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