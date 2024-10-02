using Iced.Intel;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Reg16Operand(Register Reg) : RegOperand(Reg);
}