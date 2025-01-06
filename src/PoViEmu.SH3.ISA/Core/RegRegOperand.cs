using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    public record RegRegOperand(ShRegister Reg1, ShRegister Reg2) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"rr{(v ? $"({Reg1},{Reg2})" : "")}";
        }

        public override string ToString() => $"{Reg1.Name()}:{Reg2.Name()}";
    }
}