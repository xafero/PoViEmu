using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class RegPlusArg : RegByteArg
    {
        public RegPlusArg(Register reg, byte value) : base(reg, value)
        {
        }

        public override string ToString()
        {
            return $"[{Reg}+0x{Num:x}]";
        }
    }
}