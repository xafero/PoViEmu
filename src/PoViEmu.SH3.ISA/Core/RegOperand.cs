using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    public record RegOperand(ShRegister Reg) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"r{(v ? $"({Reg})" : "")}";
        }

        public override string ToString() => $"{Reg.Name()}";
    }
}