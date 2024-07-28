using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class RegByteArg : OpArg, IByteArg
    {
        public RegByteArg(Register reg, byte value)
        {
            Reg = reg;
            Num = value;
        }

        public Register Reg { get; }
        public byte Num { get; }

        public override string ToString()
        {
            return $"{Reg}";
        }

        public byte[] Bytes => [Num];
    }
}