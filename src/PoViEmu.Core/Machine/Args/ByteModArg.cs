using System;
using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ByteModArg : OpArg
    {
        public ByteModArg(byte val, char op)
        {
            Val = val;
            Op = op;
        }

        public byte Val { get; }
        public char Op { get; }

        public override string ToString()
        {
            var val = Val.HaveComplement();
            return $"byte {Op}0x{val:x}";
        }
    }
}