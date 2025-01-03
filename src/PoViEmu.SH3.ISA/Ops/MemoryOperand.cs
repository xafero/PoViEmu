using PoViEmu.SH3.ISA.Core;

namespace PoViEmu.SH3.ISA.Ops
{
    public sealed record MemoryOperand(
        ShRegister Off,
        ShRegister? Base = null,
        bool IsPlus = false,
        bool IsMinus = false,
        ushort? Dis = null)
        : BaseOperand
    {
        public override string ToString()
        {
            var @base = Base?.Name();
            var off = Off.Name();
            var dis = Dis?.ToString();
            var suffix = IsPlus ? "+" : "";
            var prefix = IsMinus ? "-" : "";

            if (dis != null)
                return $"@({dis},{off})";

            if (@base == null)
                return $"@{prefix}{off}{suffix}";

            return $"@({off},{@base})";
        }
    }
}