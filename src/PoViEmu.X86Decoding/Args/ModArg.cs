using PoViEmu.X86Decoding.Core;
using PoViEmu.X86Decoding.Ops;
using PoViEmu.X86Decoding.Tools;

namespace PoViEmu.X86Decoding.Args
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
        public OpArg? Sec { get; }

        public override string ToString()
        {
            return $"{Mod} {Arg},{Sec}".Trim(',', ' ');
        }

        public Instruction? Parent { private get; set; }

        public byte[] Bytes => Parent.CollectBytes(Arg, Sec);
    }
}