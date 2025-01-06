using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    public record RegMathOperand(ShRegister Reg, int? Dis = null) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"rm{(v ? $"({Dis},{Reg})" : "")}";
        }

        public override string ToString() => $"@({Dis}, {Reg.Name()})";
    }
}