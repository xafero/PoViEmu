using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class BoxArg : OpArg, IByteArg
    {
        public BoxArg(Register a, byte? val)
        {
            A = a;
            Val = val;
        }

        public Register A { get; }
        public byte? Val { get; }

        public override string ToString()
        {
            return $"[{A.ToSmallName()}]";
        }

        public byte[] Bytes => Val == null ? [] : [Val.Value];
    }
}