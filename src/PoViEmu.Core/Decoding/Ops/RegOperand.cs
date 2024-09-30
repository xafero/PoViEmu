using Iced.Intel;

namespace PoViEmu.Core.Decoding.Ops
{
    public abstract record RegOperand(Register Reg) : BaseOperand;

    public record Reg8Operand(Register Reg) : RegOperand(Reg);

    public record Reg16Operand(Register Reg) : RegOperand(Reg);
}