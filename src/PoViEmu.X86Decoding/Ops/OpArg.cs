using PoViEmu.X86Decoding.Args;

namespace PoViEmu.X86Decoding.Ops
{
    public abstract class OpArg
    {
        public static implicit operator OpArg(Register v) => new RegisterArg(v);

        public static implicit operator OpArg(byte v) => new ByteArg(v, false);

        public static implicit operator OpArg(short v) => new ShortArg(v, false);
    }
}