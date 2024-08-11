using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class BoxArg : OpArg, IByteArg
    {
        public BoxArg(OpArg a, byte? val = null)
        {
            A = a;
            Val = val;
        }

        public OpArg A { get; }
        public byte? Val { get; }

        public override string ToString()
        {
            return $"[{A}]";
        }

        public byte[] Bytes => Val == null ? A is IByteArg x ? x.Bytes : [] : [Val.Value];
    }
}