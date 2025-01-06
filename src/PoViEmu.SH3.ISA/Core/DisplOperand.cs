using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    public record DisplOperand(uint Val) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"d{(v ? $"({Val})" : "")}";
        }

        public override string ToString() => $"d{Val:x}";
    }
}