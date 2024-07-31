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
            var suffix = Num == 0 ? "" : $"+0x{Num:x}";
            return $"[{Reg}{suffix}]";
        }
    }
}