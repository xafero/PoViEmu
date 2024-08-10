using System.Linq;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ModArg : OpArg, ICalcArg, IByteArg
    {
        public ModArg(Modifier mod, OpArg arg, OpArg? sec = null)
        {
            Mod = mod;
            Arg = arg;
            Sec = sec;
        }

        public Modifier Mod { get; }
        public OpArg Arg { get; }
        public OpArg Sec { get; }

        public override string ToString()
        {
            return $"{Mod} {Arg},{Sec}".Trim(',', ' ');
        }

        public Instruction? Parent
        {
            set
            {
                if (Arg is ICalcArg fa)
                    fa.Parent = value;
                if (Sec is ICalcArg sa)
                    sa.Parent = value;
            }
        }

        public byte[] Bytes
        {
            get
            {
                var first = (Arg as IByteArg)?.Bytes ?? [];
                var second = (Sec as IByteArg)?.Bytes ?? [];
                return first.Concat(second).ToArray();
            }
        }
    }
}