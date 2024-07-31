using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class RegPlusRegArg : RegPlusArg
    {
        public RegPlusRegArg(Register x, Register? y, byte value) : base(x, value)
        {
            Add = y;
        }

        public Register? Add { get; }

        public override string ToString()
        {
            var suffix = Add == null ? "" : $"+{Add}";
            return $"[{Reg}{suffix}]";
        }
    }
}