using PoViEmu.X86Decoding.Ops;

namespace PoViEmu.X86Decoding.Args
{
    public sealed class ImplicitArg : OpArg
    {
        public ImplicitArg(byte val)
        {
            Val = val;
        }

        public byte Val { get; }

        public override string ToString()
        {
            return $"{Val}";
        }
    }
}