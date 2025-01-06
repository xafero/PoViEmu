using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    public record SImmOperand(sbyte Val) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"si{(v ? $"({Val})" : "")}";
        }

        public override string ToString() => $"#{Val}";
    }
}