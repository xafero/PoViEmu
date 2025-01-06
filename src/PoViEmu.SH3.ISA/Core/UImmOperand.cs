using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    public record UImmOperand(byte Val) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"ui{(v ? $"({Val})" : "")}";
        }

        public override string ToString() => $"#{Val}";
    }
}